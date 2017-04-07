module Types
open System

type CustomerId = int
type [<Measure>] Point
type Points = float<Point>
type CustomerName = string

type GoldData = 
    {
        PromotedDate: DateTime
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
    with 
        static member create id name = {Id = id; Name = name; Points = 0.<Point>; FrequentFlyerStatus = Regular}
        member this.isGoldMember() = 
            match this.FrequentFlyerStatus with
            | Gold _ -> true
            | Regular -> false

type [<Measure>] Km
type Distance = float<Km>

type DestinationName = string
type DestinationId = int
type Destination = 
    {
        Id: int
        Name: DestinationName
    }
    with static member create id name = {Id = id; Name = name}

type [<Measure>] SEK
type Price = float<SEK>

type TicketClass = 
    | Economy
    | Business
    | FirstClass
type Ticket = 
    {
        CustomerId: CustomerId
        Price: Price
        TicketClass: TicketClass
        From: Destination
        To: Destination
    }
    with static member create id price ticketClass from toDest =
        {
            CustomerId = id
            Price = price
            TicketClass = ticketClass
            From = from
            To = toDest
        }

