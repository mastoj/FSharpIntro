#load "List.fs"
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
