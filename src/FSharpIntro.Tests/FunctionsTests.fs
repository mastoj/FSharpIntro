module FunctionsTests
open FsUnit
open FsUnit.Xunit
open Xunit
open Types

module ConversionTests = 
    open Functions.Conversion

    [<Fact>]
    let ``one SEK should equal one Point``() =
        1.<SEK>
        |> priceToPoints
        |> should equal 1.<Point>

    [<Fact>]
    let ``one km should equal 0.3 Point``() = 
        1.<Km>
        |> distanceToPoints
        |> should equal 0.3<Point>

module CustomerTests = 
    open Functions
    open Functions.Customer
    open System

    [<Fact>]
    let ``setGoldStatus should upgrade a regular customer with to days date``() = 
        let customer = Customer.create 1 "tomas"
        let expectedStatus = Gold { PromotedDate = DateTime.Today }
        customer
        |> setGoldStatus
        |> should equal {customer with FrequentFlyerStatus = expectedStatus }

    [<Fact>]
    let ``setGoldStatus should keep promoted date on gold customer``() = 
        let goldCustomer = Customer.create 1 "tomas"
        let olderGoldCustomer = { goldCustomer with FrequentFlyerStatus = Gold { PromotedDate = DateTime.Today.AddDays(-20.) } }

        olderGoldCustomer
        |> setGoldStatus
        |> should equal olderGoldCustomer

    [<Fact>]
    let ``setRegularStatus should not change a regular customer``() = 
        let customer = Customer.create 1 "tomas"
        customer
        |> setRegularStatus
        |> should equal customer

    [<Fact>]
    let ``setGoldStatus should downgrade gold customer``() = 
        let customer = Customer.create 1 "tomas"
        let olderGoldCustomer = { customer with FrequentFlyerStatus = Gold { PromotedDate = DateTime.Today.AddDays(-20.) } }

        olderGoldCustomer
        |> setRegularStatus
        |> should equal customer

    [<Fact>]
    let ``isGoldMember should return true for more than 5500 points``() =
        let customer = Customer.create 1 "tomas"
        { customer with Points = 5501.<Point> }
        |> isGoldMember
        |> should be True

    [<Fact>]
    let ``isGoldMember should return false for 5500 points or less``() =
        let customer = Customer.create 1 "tomas"
        { customer with Points = 5500.<Point> }
        |> isGoldMember
        |> should be False

    [<Fact>]
    let ``ticketToPoints should use distance and price to calculate points for a ticket``() = 
        let ticket = 
            {
                CustomerId = 1
                Price = 300.<SEK>
                TicketClass = Economy
                From = Destination.create 1 "from"
                To = Destination.create 2 "to"
            }
        let getDistance _ = 100.<Km>
        ticket
        |> ticketToPoints getDistance
        |> should equal 330.<Point>

    [<Fact>]
    let ``calculatePoints should only use last three tickets``() = 
        let createTicket price = 
            {
                CustomerId = 1
                Price = price * 1.<SEK>
                TicketClass = Economy
                From = Destination.create 1 "from"
                To = Destination.create 2 "to"
            }
        let tickets = [ for x in 1. .. 4. do yield createTicket x ]
        let ticketToPoints ticket = 
            ticket.Price |> Conversion.priceToPoints
        tickets
        |> calculatePoints ticketToPoints
        |> should equal 6.<Point>

    [<Fact>]
    let ``updateCustomer should upgrade customer if gold member check is true``() =
        let goldMemberCheck customer = 
            customer.Points > 100.<Point>

        let calculatePoints tickets = 101.<Point>

        let customer = Customer.create 1 "tomas"
        customer
        |> updateCustomer goldMemberCheck calculatePoints []
        |> (fun c -> 
            printfn "%A" c; c)

        |> should equal { customer with 
                            FrequentFlyerStatus = Gold { PromotedDate = DateTime.Today }
                            Points = 101.<Point> }

    [<Fact>]
    let ``updateCustomer should downgrade customer if gold member check is false``() =
        let goldMemberCheck customer = 
            customer.Points > 100.<Point>

        let calculatePoints tickets = 100.<Point>

        let customer = Customer.create 1 "tomas"
        let goldCustomer = { customer with FrequentFlyerStatus = Gold { PromotedDate = DateTime.Today } }
        goldCustomer
        |> updateCustomer goldMemberCheck calculatePoints []
        |> (fun c -> 
            printfn "%A" c; c)

        |> should equal { customer with 
                            FrequentFlyerStatus = Regular
                            Points = 100.<Point> }

module TicketsTests = 
    open Functions
    open Tickets

    [<Fact>]
    let ``createTicket return None if price is not found``() = 
        let getPrice _ = None
        let fromDest = Destination.create 1 "from"
        let toDest = Destination.create 1 "to"

        createTicket getPrice 1 fromDest toDest Economy
        |> should equal None

    [<Fact>]
    let ``createTicket return Some ticket if price is found``() = 
        let price = 100.<SEK>
        let getPrice _ = Some 100.<SEK>
        let fromDest = Destination.create 1 "from"
        let toDest = Destination.create 1 "to"
        let customerId = 1
        let ticketClass = Economy
        let expectedTicket = Ticket.create customerId price ticketClass fromDest toDest

        createTicket getPrice customerId fromDest toDest ticketClass
        |> should equal (Some expectedTicket)

    [<Fact>]
    let ``upgradeTicket should upgrade ticket if possible``() =
        let fromDest = Destination.create 1 "from"
        let toDest = Destination.create 1 "to"
        let classAndExpected = [(Economy, Business);(Business, FirstClass);(FirstClass, FirstClass)]
        classAndExpected
        |> List.map (fun (current, expected) -> 
            let ticket = Ticket.create 1 1.<SEK> current fromDest toDest
            ticket
            |> upgradeClass
            |> should equal { ticket with TicketClass = expected })

    [<Fact>]
    let ``tryUpgradeTicket should upgrade ticket for gold gustomers``() = 
        let fromDest = Destination.create 1 "from"
        let toDest = Destination.create 1 "to"
        let ticket = Ticket.create 1 1.<SEK> Economy fromDest toDest
        let customer = Customer.create 1 "tomas"
        let goldCustomer = { customer with FrequentFlyerStatus = Gold { PromotedDate = System.DateTime.Today } }
        ticket
        |> tryUpgradeTicketForCustomer goldCustomer
        |> should equal { ticket with TicketClass = Business }

    [<Fact>]
    let ``tryUpgradeTicket should not upgrade ticket for non gold gustomers``() = 
        let fromDest = Destination.create 1 "from"
        let toDest = Destination.create 1 "to"
        let ticket = Ticket.create 1 1.<SEK> Economy fromDest toDest
        let customer = Customer.create 1 "tomas"
        ticket
        |> tryUpgradeTicketForCustomer customer
        |> should equal ticket
