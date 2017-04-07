module Data

open Types

module Destinations = 
    let private destinationList = 
        [ "Barcelona"; "Stockholm"; "Oslo" ] 
        |> List.mapi Destination.create
    let private distanceList = 
        [ (0,1,2789.2); (0,2,2141.16); (1,2,530.) ]
        |> List.collect (fun (index1, index2, distance) -> 
            let typedDistance = distance |> createDistance
            [
                (destinationList.[index1], destinationList.[index2]), typedDistance
                (destinationList.[index2], destinationList.[index1]), typedDistance
            ])
        |> Map.ofList
    
    let private priceList = 
        [ (0,1,700., 1500., 2000.); (0, 2, 600., 1300., 1800.); (1, 2, 400., 700., 1000. ) ]
        |> List.collect (fun (index1, index2, economyPrice, businessPrice, firstClassPrice) ->
            let [typedEconomyPrice; typedBusinessPrice; typedFirstClassPrice] = 
                [economyPrice; businessPrice; firstClassPrice] |> List.map createPrice
            [
                (destinationList.[index1], destinationList.[index2]), (typedEconomyPrice, typedBusinessPrice, typedFirstClassPrice)
                (destinationList.[index2], destinationList.[index1]), (typedEconomyPrice, typedBusinessPrice, typedFirstClassPrice)
            ])
        |> Map.ofList

    let getDestinations() = destinationList
    let getDestination destinationId = destinationList |> List.tryFind (fun dest -> dest.Id = destinationId)
    let getDistance key = distanceList |> Map.find key
    let getPrice (key, ticketClass) = 
        priceList
        |> Map.tryFind key
        |> Option.bind (fun (economyPrice, businessPrice, firstClassPrice) ->
            match ticketClass with
            | Economy -> economyPrice
            | Business -> businessPrice
            | FirstClass -> firstClassPrice
            |> Some)

module Tickets = 
    let mutable private tickets : Ticket list = []
    let saveTicket ticket = tickets <- ticket :: tickets
    let getTicketsForCustomer customerId = 
        tickets
        |> List.filter (fun t -> t.CustomerId = customerId)

module Customers = 
    let mutable customerList : Customer list = []

    let getCustomer predicate = customerList |> List.tryFind predicate

    let getCustomerById customerId = getCustomer (fun c -> c.Id = customerId)

    let getCustomerByName customerName = getCustomer (fun c -> c.Name = customerName)

    let getCustomers() = customerList

    let addCustomer customerName =
        match customerName |> getCustomerByName, customerList with
        | None, [] -> 
            Customer.create 0 customerName
            |> Some
        | None, customers -> 
            let customerId = (customerList |> List.map (fun c -> c.Id) |> Seq.max)
            Customer.create (customerId  + 1) customerName
            |> Some
        | Some c, _ -> None
        |> Option.bind (fun c ->
            customerList <- c::customerList
            Some c)

    let updateCustomer (customer:Customer) = 
        customerList <- customerList |> List.map (fun c -> if c.Id = customer.Id then customer else c)
        