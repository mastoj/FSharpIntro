open System

module Types = 
    type CustomerId = int
    type [<Measure>] Point
    type Points = float<Point>
    type CustomerName = string

    type GoldData = 
        {
            PromotedData: DateTime
        }

    type FrequentFlyerStatus = 
        | Regular
        | Gold of GoldData

    type Customer = 
        {
            Id: CustomerId
            Name: CustomerName
            Points: Points
            FrequentFlyerStatus: FrequentFlyerStatus
        }

    type [<Measure>] SEK
    type Price = float<SEK>
    let createPrice p = p * 1.<SEK>

    type [<Measure>] Km
    type Distance = float<Km>
    let createDistance d = d * 1.<Km>

    type DestinationName = string
    type DestinationId = int
    type Destination = 
        {
            Id: int
            Name: DestinationName
        }
        with static member create id name = { Id = id; Name = name }

    type Class = 
        | Economy
        | Business
        | FirstClass
    type Ticket = 
        {
            Customer: Customer
            Price: Price
            Class: Class
            From: Destination
            To: Destination
        }

module Functions = 
    open Types

    let priceToPoints price = 
        price / 0.5<SEK/Point>

    let distanceToPoints distance = 
        distance / 1.<Km/Point>

    let isGoldMember predicate getTickets customer = 
        getTickets customer.Id
        |> predicate

    let goldTraveler getDistance goldDistanceLimit tickets = 
        let totalMiles : Distance = 
            tickets
            |> List.take 10
            |> List.sumBy (fun t -> (t.From, t.To) |> getDistance |> Option.get)
        totalMiles >= goldDistanceLimit

    let goldSpender getSpending goldSpendingLimit tickets = 
        let totalSpending : Price = 
            tickets
            |> List.take 10
            |> List.sumBy (fun t -> t.Price)

        totalSpending >= goldSpendingLimit

    let goldMemberPredicate predicates tickets = 
        predicates
        |> List.fold (fun isGoldMember predicate -> isGoldMember || (predicate tickets)) false

    let upgradeClass ticket = 
        match ticket.Class with
        | Economy -> { ticket with Class = Business }
        | Business -> { ticket with Class = FirstClass }
        | FirstClass -> { ticket with Class = FirstClass }

    let tryUpgradeTicketForCustomer getTickets goldMemberCheck gold customer ticket = 
        if goldMemberCheck getTickets customer
        then upgradeClass ticket
        else ticket

module Map =
    let findOrDefault key defaultValue map =
        match map |> Map.tryFind key with
        | None -> defaultValue
        | Some x -> x

module Data = 
    open Types

    let destinationList = 
        [ "Barcelona"; "Stockholm"; "Oslo" ] 
        |> List.mapi Destination.create
    let distanceList = 
        [ (0,1,2789.2); (0,2,2141.16); (1,2,530.) ]
        |> List.collect (fun (index1, index2, distance) -> 
            let typedDistance = distance |> createDistance
            [
                (destinationList.[index1], destinationList.[index2]), typedDistance
                (destinationList.[index2], destinationList.[index1]), typedDistance
            ])
        |> Map.ofList
    
    let priceList = 
        [ (0,1,700.); (0,2,600.); (1,2,400.) ]
        |> List.collect (fun (index1, index2, price) ->
            let typedPrice = price |> createPrice
            [
                (destinationList.[index1], destinationList.[index2]), typedPrice
                (destinationList.[index2], destinationList.[index1]), typedPrice
            ])
        |> Map.ofList

    let mutable tickets : Map<CustomerId, Ticket list> = Map.empty
    let addTicket ticket customerId = 
        let existingTickets = Map.findOrDefault customerId [] tickets
        tickets <- Map.add customerId (ticket::existingTickets) tickets
        tickets

    let getTickets customerId = Map.findOrDefault customerId [] tickets

    let customerList = 
        ["Tomas"; "John"; "Doe"]
        |> List.mapi (fun id name ->
            id,
            {
                Id = id
                Name = name
                Points = 0.<Point>
                FrequentFlyerStatus = Regular
            })
        |> Map.ofList
    let getCustomer customerId = customerList |> Map.tryFind customerId
    let getCustomers() = customerList |> Map.toList |> List.map snd

module Services =
    open Data
    open Types

    type DestinationService() = 
        member this.GetDestinations() = 
            destinationList
        member this.GetDistance ((destination1, destination2) as key) = 
            distanceList |> Map.tryFind key
        member this.GetPrice ((destination1, destination2) as key) = 
            priceList |> Map.tryFind key
    
    type TicketService() = 
        member this.GetTicketsForCustomer customerId : Ticket list = []

    type CustomerService() = 
        member this.GetCustomer customerId : Customer option = 
            getCustomer customerId
        member this.GetCustomers() = getCustomers()
            
            
module Application = 
    open Services
    open Types

    type App = 
        {
            getCustomers: unit -> Customer list
            //reserveTicket: unit -> Ticket
        }
    let createApp (customerService: CustomerService) (ticketService: TicketService) (destinationService: DestinationService) = 
        {
            getCustomers = customerService.GetCustomers
        }

open Services
let app = Application.createApp (new CustomerService()) (new TicketService()) (new DestinationService())
app.getCustomers()

    //    fun customerId destinationId1 destinationId2 = 
    //        customerService.GetCustomer customerId
    //        |> Option.bind 

    //let registerTicket customerId destinationId1 destinationId2 = 
    //        match 