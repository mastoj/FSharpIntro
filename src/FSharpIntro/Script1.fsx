open System

module Types = 
    type CustomerId = int
    type [<Measure>] Point
    type Points = float<Point>

    type CustomerData = 
        {
            Id: CustomerId
            Points: Points 
        }

    type RegularData = CustomerData

    type FrequentFlyerData = 
        {
            CustomerData: CustomerData
            PromotedData: DateTime
        }

    type Customer = 
        | Regular of RegularData
        | FrequentFlyer of FrequentFlyerData

    type [<Measure>] SEK
    type Price = float<SEK>

    type [<Measure>] Km
    type Distance = float<Km>
    let createDistance d = d * 1.<Km>

    type Class = 
        | Economy
        | Business
        | FirstClass

    type DestinationName = string
    type Destination = 
        {
            Name: DestinationName
        }
        with static member create name = { Name = name }

    type Ticket = 
        {
            Customer: Customer
            Price: Price
            Class: Class
            From: Destination
            To: Destination
        }

module Data = 
    open Types

    let destinationList = 
        [ "Barcelona"; "Stockholm"; "Oslo" ] 
        |> List.map Destination.create
    let distanceList = 
        [ (0,1,2789.2); (0,2,2141.16); (1,2,530.) ]
        |> List.collect (fun (index1, index2, distance) -> 
            let typedDistance = distance |> createDistance
            [
                (destinationList.[index1], destinationList.[index2]), typedDistance
                (destinationList.[index2], destinationList.[index1]), typedDistance
            ])
        |> Map.ofList

module Functions = 
    open Types

    let priceToPoints price = 
        price / 0.5<SEK/Point>

    let distanceToPoints distance = 
        distance / 1.<Km/Point>
