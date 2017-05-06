﻿module Program
open Services
open System
open Data
open Types
open Application

let app = Application.createApp (new CustomerService()) (new TicketService()) (new DestinationService())

[<EntryPoint>]
let rec main argv =
    let what = (Console.ReadLine()).Split(' ') |> List.ofArray
    match what with
    | ["ac"; name] ->
        app.addCustomer name |> printfn "%A"
        main [||]
    | ["rt"; customerId; ticketClass; fromId; toId] ->
        try
            let parsedCustomerId = Int32.Parse(customerId)
            let parsedTicketClass = parseTicketClass ticketClass
            let parsedFromId = Int32.Parse(fromId)
            let parsedToId = Int32.Parse(toId)

            app.reserveTicket 
                { 
                    CustomerId = parsedCustomerId
                    TicketClass = parsedTicketClass
                    From = parsedFromId
                    To = parsedToId
                }
            |> printfn "%A"
        with
        | _ -> printfn "FAILED TO PARSE COMMAND"
        main [||]
    | ["gc"] -> 
        app.getCustomers() |> printfn "%A"
        main [||]
    | ["gt"; customerId] -> 
        try
            let parsedCustomerId = Int32.Parse(customerId)
            app.getTickets parsedCustomerId
            |> printfn "%A"
        with
        | _ -> printfn "FAILED TO PARSE COMMAND"
        main [||]
    | ["q"] -> 0
    | _ -> 
        printfn "Invalid command"
        main [||]

