module Functions

open System
open Types

module Conversion = 
    
    let priceToPoints (price: float<SEK>)= price * 1.<Point/SEK>
    let distanceToPoints (distance: float<Km>) = distance * 0.3<Point/Km>
    let priceToPoints price = 
        price * 1.<Point/SEK>

    let distanceToPoints distance = 
        distance * 0.3<Point/Km>

module Customer = 
    let setGoldStatus customer = 
        match customer.FrequentFlyerStatus with
        | Regular -> { customer with FrequentFlyerStatus = Gold{PromotedDate = DateTime.Today}}
        | Gold _ -> customer

    let setRegularStatus customer = 
        match customer.FrequentFlyerStatus with
        | Regular -> customer
        | Gold _ -> { customer with FrequentFlyerStatus = Regular}

    let isGoldMember customer = 
        customer.Points > 5500.<Point>

    let ticketToPoints getDistance ticket = 
        (ticket.Price |> Conversion.priceToPoints) + 
        (getDistance (ticket.From, ticket.To) |> Conversion.distanceToPoints)

    let calculatePoints (ticketToPoints: Ticket -> float<Point>) tickets = 
        tickets
        |> List.takeXOrAll 3
        |> List.sumBy (ticketToPoints)
    
    let updateCustomer goldMemberCheck calculatePoints tickets customer = 
        let points = tickets |> calculatePoints
        let customer = {customer with Points = points}
        if customer|> goldMemberCheck
        then setGoldStatus customer
        else setRegularStatus customer
   

module Tickets = 
    let createTicket getPrice customerId fromDestination toDestination ticketClass =
        getPrice ((fromDestination, toDestination), ticketClass)
        |> Option.bind (fun price ->
            Ticket.create customerId price ticketClass fromDestination toDestination
            |> Some
        )

    let upgradeClass ticket = 
        match ticket.TicketClass with
        | Economy -> { ticket with TicketClass = Business }
        | Business -> { ticket with TicketClass = FirstClass }
        | FirstClass -> ticket

    let tryUpgradeTicketForCustomer (customer:Customer) ticket = 
        if customer.isGoldMember()
        then upgradeClass ticket
        else ticket
        else ticket
