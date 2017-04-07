module Services

open Types
open Data.Customers
open Data.Destinations
open Data.Tickets

type DestinationService() = 
    member this.GetDestinations = 
        getDestinations
    member this.GetDestination destinationId = 
        getDestination destinationId
    member this.GetDistance ((destination1, destination2) as key) = 
        getDistance key
    member this.GetPrice ((destination1, destination2) as key, ticketClass) = 
        getPrice (key, ticketClass)
    
type TicketService() = 
    member this.GetTicketsForCustomer customerId : Ticket list = 
        getTicketsForCustomer customerId
    member this.SaveTicket ticket = 
        saveTicket ticket

type CustomerService() = 
    member this.GetCustomer customerId : Customer option = 
        getCustomerById customerId
    member this.GetCustomers() = getCustomers()
    member this.AddCustomer customerName = 
        addCustomer customerName
    member this.UpdateCustomer customer = 
        updateCustomer customer
