// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open Suave
open Suave.Operators
open Suave.WebPart
open Suave.Filters
open Suave.Successful
open Suave.RequestErrors
open Services
open Newtonsoft.Json

let toJson o = JsonConvert.SerializeObject o
let fromJson<'T> str = JsonConvert.DeserializeObject<'T>(str)
let app = Application.createApp (new CustomerService()) (new TicketService()) (new DestinationService())

module ApiTypes = 
    type CreateUserRequest = 
        {
            name: string
        }
    type ReserveTicketRequest = 
        {
            customerId: int
            fromId: int
            toId: int
            ticketClass: string
        }

module Api = 
    open Application
    open ApiTypes

    open Suave.Writers

    let getCustomers = 
        request(fun _ ->
            app.getCustomers() |> toJson |> OK)

    let createCustomer = 
        request(fun r ->
            r.rawForm
            |> System.Text.UTF8Encoding.UTF8.GetString
            |> fromJson<CreateUserRequest>
            |> (fun cr -> app.addCustomer cr.name)
            |> function 
                | None -> BAD_REQUEST "Invalid user name"
                | Some c -> c |> toJson |> OK)

    let customerApi = 
        choose [
            GET >=> path "/customers" >=> getCustomers
            POST >=> path "/customer" >=> createCustomer
        ]

    let ticketApi =
        let getTickets customerId =
            app.getTickets customerId
            |> toJson
            |> OK

        let reserveTicket =
            let toApplicationType tr = 
                {
                    CustomerId = tr.customerId
                    From = tr.fromId
                    To = tr.toId
                    TicketClass = (parseTicketClass tr.ticketClass)
                }

            request(fun r ->
                r.rawForm
                |> System.Text.UTF8Encoding.UTF8.GetString
                |> fromJson<ApiTypes.ReserveTicketRequest>
                |> toApplicationType
                |> (fun cr -> app.reserveTicket cr)
                |> function 
                    | None -> BAD_REQUEST "Invalid ticket request"
                    | Some c -> c |> toJson |> OK)

        choose [
            GET >=> pathScan "/customers/%i/tickets" getTickets
            POST >=> path "/ticket" >=> reserveTicket
        ]

    let api : WebPart = 
        choose
            [
                customerApi
                ticketApi
                NOT_FOUND "Invalid url"
            ] >=> setMimeType "application/json"


[<EntryPoint>]
let main argv = 
    startWebServer defaultConfig Api.api
    printfn "%A" argv
    0 // return an integer exit code
