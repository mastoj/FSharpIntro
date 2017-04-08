open System

type CustomerId = int
type Points = Points

type CustomerData = 
    {
        Id: CustomerId
        Points: Points 
    }

type RegularData = CustomerData

type FrequentFlyerData = 
    {
        CustomerData: CustomerData
        PromotedData: DateTime
    }

type Customer = 
    | Regular of RegularData
    | FrequentFlyer of FrequentFlyerData

type [<Measure>] SEK
type Price = float<SEK>

type Class = 
    | Economy
    | Business
    | FirstClass

type DestinationName = string
type Location = 
    {
        Latitude: float
        Longitude: float
    }
type Destination = 
    {
        Name: DestinationName
        Location: Location
    }

type Ticket = 
    {
        Customer: Customer
        Price: Price
        Class: Class
        From: Destination
        To: Destination
    }