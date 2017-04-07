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
﻿
﻿
﻿#load "Types.fs
﻿#load "List.fs"
#load "Types.fs"
#load "Functions.fs"
#load "Data.fs"
#load "Services.fs"
#load "Application.fs"

open System

open Services
let app = Application.createApp (new CustomerService()) (new TicketService()) (new DestinationService())
app.getCustomers() |> printfn "%A"
app.addCustomer "Tomas 2"
app.getCustomers() |> printfn "%A"
app.reserveTicket { CustomerId = 1; TicketClass = Types.TicketClass.Economy; From = 1; To = 2 }
