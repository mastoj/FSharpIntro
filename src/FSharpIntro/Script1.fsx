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

type [<Measure>] Km
type Distance = float<Km>

type DestinationName = string
type DestinationId = int
type Destination = 
    {
        Id: int
        Name: DestinationName
    }

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
﻿
﻿
﻿#load "Types.fs