module TypeTests

//open FsUnit
//open FsUnit.Xunit
//open Xunit
//open System
//open Types

//module CustomerTests = 
//    [<Fact>]
//    let ``create returns default customer with id set``() =
//        Customer.create 1 "tomas"
//        |> should equal { Id = 1; Name = "tomas"; Points = 0.<Point>; FrequentFlyerStatus = Regular }

//    [<Fact>]
//    let ``gold member check returns false for regular customer``() =
//        let regularCustomer = Customer.create 1 "tomas"
//        regularCustomer.isGoldMember()
//        |> should be False

//    [<Fact>]
//    let ``gold member check returns true for gold customer``() =
//        let regularCustomer = Customer.create 1 "tomas"
//        let goldCustomer = { regularCustomer with FrequentFlyerStatus = Gold { PromotedDate = DateTime.Now } }
//        goldCustomer.isGoldMember()
//        |> should be True
        
//module DestinationTests = 
//    [<Fact>]
//    let ``create destination creates destination``() = 
//        Destination.create 3 "a destination"
//        |> should equal { Id = 3; Name = "a destination" }

//module TicketTests = 
//    [<Fact>]
//    let ``create ticket creates ticket``() = 
//        Ticket.create 1 300.<SEK> Economy (Destination.create 1 "from") (Destination.create 2 "to")
//        |> should equal 
//            {
//                CustomerId = 1
//                Price = 300.<SEK>
//                TicketClass = Economy
//                From = Destination.create 1 "from"
//                To = Destination.create 2 "to"
//            }
open FsUnit
open FsUnit.Xunit
open Xunit
open System
open Types

module CustomerTests = 
    [<Fact>]
    let ``create returns default customer with id set``() =
        Customer.create 1 "tomas"
        |> should equal { Id = 1; Name = "tomas"; Points = 0.<Point>; FrequentFlyerStatus = Regular }

    [<Fact>]
    let ``gold member check returns false for regular customer``() =
        let regularCustomer = Customer.create 1 "tomas"
        regularCustomer.isGoldMember()
        |> should be False

    [<Fact>]
    let ``gold member check returns true for gold customer``() =
        let regularCustomer = Customer.create 1 "tomas"
        let goldCustomer = { regularCustomer with FrequentFlyerStatus = Gold { PromotedDate = DateTime.Now } }
        goldCustomer.isGoldMember()
        |> should be True
        
module DestinationTests = 
    [<Fact>]
    let ``create destination creates destination``() = 
        Destination.create 3 "a destination"
        |> should equal { Id = 3; Name = "a destination" }

module TicketTests = 
    [<Fact>]
    let ``create ticket creates ticket``() = 
        Ticket.create 1 300.<SEK> Economy (Destination.create 1 "from") (Destination.create 2 "to")
        |> should equal 
            {
                CustomerId = 1
                Price = 300.<SEK>
                TicketClass = Economy
                From = Destination.create 1 "from"
                To = Destination.create 2 "to"
            }
