module Application

open Functions.Tickets
open Functions.Customer
open Services
open Types
open Functions

type ReserveTicketRequest = 
    {
        CustomerId: CustomerId
        From: DestinationId
        To: DestinationId
        TicketClass: TicketClass
    }
type App = 
    {
        getCustomers: unit -> Customer list
        addCustomer: CustomerName -> Customer option
        reserveTicket: ReserveTicketRequest -> (Customer * Ticket) option
        getTickets: CustomerId -> Ticket list
    }

module Option = 
    let (>>=) opt f = 
        match opt with
        | Some x -> f x
        | None -> None
open Option

let reserveTicket 
        (customerService: CustomerService) 
        (ticketService: TicketService) 
        (destinationService: DestinationService) reserveTicketRequest = 
    let tryUpgradeTicketForCustomer (customer, ticket) 
        : (Customer * Ticket) option =
        tryUpgradeTicketForCustomer customer ticket
        |> (fun t -> Some (customer, t))

    let createTicket 
        reserveTicketRequest
        ((customer: Customer), fromDestination, toDestination) 
        : (Customer * Ticket) option = 
        createTicket 
            destinationService.GetPrice
            reserveTicketRequest.CustomerId 
            fromDestination 
            toDestination 
            reserveTicketRequest.TicketClass
        >>= (fun t -> Some (customer, t))
           
    let getFromDestionation reserveTicketRequest customer 
        : (Customer * Destination) option = 
        destinationService.GetDestination reserveTicketRequest.From
        >>= (fun from -> Some (customer, from))
        
    let getToDestination reserveTicketRequest (customer, from) 
        : (Customer * Destination * Destination) option = 
        destinationService.GetDestination reserveTicketRequest.To
        >>= (fun toDest -> Some (customer, from, toDest))

    let updateCustomer ((customer:Customer), ticket)
        : (Customer * Ticket) option = 
        let tickets = ticketService.GetTicketsForCustomer customer.Id
        let calculatePoints = calculatePoints (ticketToPoints destinationService.GetDistance)
        let customer = updateCustomer isGoldMember calculatePoints tickets customer
        Some (customer, ticket)

    let saveTicket (customer, ticket) 
        : (Customer * Ticket) option = 
        ticketService.SaveTicket ticket
        Some (customer, ticket)

    let saveCustomer (customer, ticket) = 
        customerService.UpdateCustomer customer
        Some (customer, ticket)

    customerService.GetCustomer reserveTicketRequest.CustomerId
    >>= (getFromDestionation reserveTicketRequest)
    >>= (getToDestination reserveTicketRequest)
    >>= (createTicket reserveTicketRequest)
    >>= tryUpgradeTicketForCustomer
    >>= saveTicket
    >>= updateCustomer
    >>= saveCustomer

let createApp (customerService: CustomerService) (ticketService: TicketService) (destinationService: DestinationService) = 
    {
        getCustomers = customerService.GetCustomers
        addCustomer = customerService.AddCustomer
        reserveTicket = 
            (reserveTicket customerService ticketService destinationService)
        getTickets = ticketService.GetTicketsForCustomer
    }
