module DataTests

//open Data
//open FsUnit
//open FsUnit.Xunit
//open Types
//open Xunit
//open Data.Customers
//open Types


//module DestinationsTests = 
//    open Destinations

//    let expected = [
//        {
//            Id = 0
//            Name = "Barcelona"
//        }
//        {
//            Id = 1
//            Name = "Stockholm"
//        }
//        {
//            Id = 2
//            Name = "Oslo"
//        }]

//    [<Fact>]
//    let ``getDestinations should return the correct destinations``() = 

//        getDestinations()
//        |> should equal expected

//    [<Fact>]
//    let ``getDestination should return some destination for correct ids``() = 
//        expected
//        |> List.map (fun d -> d, getDestination d.Id)
//        |> List.iter (fun (expected, actual) -> 
//            actual
//            |> should equal (Some expected))

//    [<Fact>]
//    let ``getDestinations should return None for invalid ids``() =
//        99
//        |> getDestination
//        |> should equal None

//    [<Fact>]
//    let ``getDistance should return the correct distance``() = 
//        [ (0,1,2789.2); (0,2,2141.16); (1,2,530.) ]
//        |> List.iter (fun (fromId, toId, distance) ->
//            (getDestination fromId |> Option.get, getDestination toId |> Option.get)
//            |> getDistance
//            |> should equal (distance * 1.<Km>))

//    [<Fact>]
//    let ``getPrice should return the correct price for each class``() = 
//        [ (0,1,700., 1500., 2000.); (0, 2, 600., 1300., 1800.); (1, 2, 400., 700., 1000. ) ]
//        |> List.iter (fun (fromId, toId, ePrice, bPrice, fPrice) ->
//            let fromDest = getDestination fromId |> Option.get
//            let toDest = getDestination toId |> Option.get
//            [(Economy, ePrice); (Business, bPrice); (FirstClass, fPrice) ]
//            |> List.iter (fun (ticketClass, expectedPrice) ->
//                getPrice ((fromDest, toDest), ticketClass)
//                |> should equal (Some (expectedPrice * 1.<SEK>))))

//module TicketsTests = 
//    open Data.Tickets
//    open Functions

//    [<Fact>]
//    let ``saveTicket should add one ticket``() = 
//        getTicketsForCustomer 1
//        |> should equal ([]: Ticket list)
//        let ticket = Ticket.create 1 300.<SEK> Economy (Destination.create 1 "from") (Destination.create 2 "to")
//        ticket |> saveTicket
//        getTicketsForCustomer 1
//        |> should equal [ticket]

//type CustomersTests() = 
//    do 
//        Customers.customerList <- []

//    with 
//        [<Fact>]
//        member this.``addCustomer should add one customer if it doesn't exist``() = 
//            getCustomers()
//            |> should equal ([]: Customer list)

//            "tomas"
//            |> addCustomer
//            |> should equal (Some (Customer.create 0 "tomas"))

//            getCustomers()
//            |> should equal [(Customer.create 0 "tomas")]

//        [<Fact>]
//        member this.``addCustomer should not change existing customer if it exist``() = 
//            "tomas"
//            |> addCustomer
//            |> ignore

//            "tomas"
//            |> addCustomer
//            |> should equal None

//            getCustomers()
//            |> should equal [Customer.create 0 "tomas"]

//        [<Fact>]
//        member this.``getCustomerById should return based on id``() = 
//            "tomas"
//            |> addCustomer
//            |> ignore

//            "emrik"
//            |> addCustomer
//            |> ignore

//            getCustomerById 0
//            |> should equal (Some (Customer.create 0 "tomas"))

//            getCustomerById 1
//            |> should equal (Some (Customer.create 1 "emrik"))

//        [<Fact>]
//        member this.``getCustomerByName should return based on name``() = 
//            "tomas"
//            |> addCustomer
//            |> ignore

//            "emrik"
//            |> addCustomer
//            |> ignore

//            getCustomerByName "tomas"
//            |> should equal (Some (Customer.create 0 "tomas"))

//            getCustomerByName "emrik"
//            |> should equal (Some (Customer.create 1 "emrik"))
        
//        [<Fact>]
//        member this.``updateCustomer should update only customer with the matching id``() = 
//            "tomas"
//            |> addCustomer
//            |> ignore

//            "emrik"
//            |> addCustomer
//            |> ignore

//            let (Some tomas) = getCustomerByName "tomas"
            
//            {tomas with Points = 69.<Point> }
//            |> updateCustomer

//            getCustomerByName "emrik"
//            |> should equal (Some (Customer.create 1 "emrik"))
        
//            getCustomerByName "tomas"
//            |> should equal (Some {tomas with Points = 69.<Point>})
