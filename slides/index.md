- title : FSharpIntro
- description : Introduction to F#
- author : Tomas Jansson
- theme : night
- transition : default

***

<div class="intro-slide">

# FSharpIntro

## An introduction to the beautiful world of F#

<div class="author">
    <div>@TomasJansson</div>
</div>

</div>

*** 

## Agenda of the workshop

<div class="content">

* A brief introduction to what F# is
* Introduction to exercises
* Module 1 - Modelling with types, and get used to the tooling
* Module 2 - Functions, operators, pattern matching, option and little bit of lists
* Module 3 - More options, lists
* Module 4 - Maps and basic OO
* Module 5 - Composistion

</div>

***

<div class="intro-slide">

# What is <strong>F#</strong>?

</div>

---

## Open source <strong>functional-first</strong> programming language

* Functional code is easier to write than object oriented code

---

## <strong>Declarative</strong> over imperative

* The code focus more on <strong>what</strong> to do, instead of how to do it

---

## <strong>Concise</strong>

* Better type system allows for less <strong>noise</strong>

---

## Better <strong>defaults</strong>

* Non-null by default - <strong>fewer unexpected bugs</strong>
* Immutable - <strong>makes it easier to reason</strong>

***

<div class="intro-slide">

# Exercises

## Goal, structure and exercise description

</div>

---

## Goal

<div class="content">

* F# fundamentals
* Scripts and applications
* Functional concepts

</div>

---

## Structure

<div class="content">

* 5 different modules
* Each module focus on different topics (but might borrow from each other)
* Theory between each set of exercises
* Exercises that covers most of the theory

</div>

---

## Exercise description

<div class="content">

* Flight ticketing application
* Use cases:
    - Add user
    - List users
    - Reserve ticket
    - Ticket should be automatically upgraded for gold members
    - Point for distance and spending
    - Point might give you gold member status
    - List tickets for customer

</div>

***

<div class="intro-slide">

# Module 1

## Modelling with types, and get used to the tooling

</div>

---

## The tools

<div class="content">

* REPL (Read Evaluate Print Loop)
    - End line with `;;` to mark the end of the input
    - `C:\Program Files (x86)\Microsoft SDKs\F#\4.1\Framework\v4.0\fsi.exe` (tip: add to path)
* Visual Studio
    - `Alt+Enter` - send selection to REPL/Interactive window
* Visual Studio code
    - `Alt+Enter` - send selection to REPL/Interactive window

</div>

---

## Script files

<div class="content">

* You can reference dll files, other script and source files
* Use it to explore code quickly without a project
* Use `open` instead of `using` to reference a namespace


    [lang=fsharp]
    #r "APathToA.dll"
    #load "APathToAnotherScript.fsx"
    #load "APathToASourceFile.hs"

    open System

    let x = 10
    let time = DateTime.Now

* Execute in two ways:
    * `Alt+Enter` in IDE
    * `fsi --exec file.fsx`

</div>

---

## Applications

<div class="content">

Same as for C# but with some minor changes

* Source code are kept in `.fs` files
* The project file is a `.fsproj` instead of `.csproj`
* <strong>File ordering matter</strong>, files must be in dependency order in the project file
* F# projects can reference C# dlls
* C# projects can reference F# dlls
* You can mix F# and C# in one solution

</div>

---

## Expressions

<div class="content">

Everything in F# is an expression

    let x = if true then 100

* <strong>Doesn't</strong> compile since if is an <strong>expression</strong>, not a statement like in C#.

* Return values are used instead of mutation

* (in the sample above the else clause has an implicit `unit` return value)

</div>

---

## Bindings

<div class="content">

When assigning a variable a value, you <strong>bind</strong> that variable to the value. The value can be a function or an actual value

    // bind the value 5 to the variable x
    let x = 5

    // bind the function x + y to add
    let add = fun x y -> x + y

    // false, here it is a pattern match and not bind
    x = x + 1

</div>

---

## Types 1

<div class="content">

* Built in types (those that are used in the workshop)


    let x = 1                   // int
    let y = 1.                  // the . indicates that this is a float
    let z = true                // bool
    let w = "tomas"             // string
    let v = System.DateTime.Now // DateTime

* Type aliases


    type MyString = string
    type MyInt = int

</div>

---

## Types 2

<div class="content">

* Record types


    type Author = {Name: string; Age: int}
    let tomas = {Name = "tomas"; Age = 35}
    let tomas2 = 
        {
            Name = "tomas"
            Age = 35
        }
    tomas = tomas2 // true, because of structural comparison
                   // and = is equal and not assignment
    // Create new record based on old one
    let tomasInOneYear = {tomas with Age = tomas.Age + 1}
    tomas = tomasInOneYear // false

* Tuples


    let x = ("tomas", 35)
    let (name, age) = x

</div>

---

## Types 3

<div class="content">

* Discriminated union


    type Shape = 
        | Rectangle of int * int
        | Circle of Radius:int

* Function types (finally some functions)


    // myFun : x:int -> y:int -> int (this is the type of the function)
    let myFun x y = x + y

* Measure types


    type [<Measure>] Km
    type [<Measure>] Hour
    let x = 10.<Km>/1.<Hour> // x: float<Km/Hour>

</div>

---

## Gotchas

<div class="content">

Now when you've seen some code it is time for some gotchas:

* <strong>Order matter</strong>
* <strong>Structure matter</strong> (F# uses signficant space)
* F# types uses <strong>structural equality</strong> as default
* F# types are <strong>immutable</strong> by default
* F# types are <strong>not nullable</strong> by default
* F# has good <strong>type inference</strong> (but sometimes you need to help the compiler)


    //         in type1   in type2    return type
    //            |          |           |
    //            V          V           V
    let myFun (x:string) (y:string) : string = x + y

</div>

***

<div class="intro-slide">

# <strong>Exercises</strong> - Module 1

</div>

---

## Goal

<div class="content">

* Learn how to create types
* Learn how to use the FSI to experiment
* Try to play around with the code while going through the exercises

</div>

---

## Exercise 1.1 - getting started

<div class="content">

* Clone the github repo: https://github.com/mastoj/FSharpIntro
* Open branch module1
* Open the solution file in the `src` folder
* Open `script.fsx`
* Create the following types:

</div>

---

## Exercise 1.2 - create customer types

<div class="content">

* Type alias named `CustomerId` of type `int`
* Measure type called `Point`
* `Points` type of type `float<Point>`
* Type alias `CustomerName` of type `string`
* Record type `GoldData` that has a property `PromotedDate` of type `DateTime`

</div>

---

## Exercise 1.2 - create customer types (cont.)

<div class="content">

* Discriminated union type `FrequentFlyerStatus` with two cases
    * `Regular` with no field
    * `Gold` with one field of type `GoldData`
* Record type `Customer` that has the following properties
    * `Id` of type `CustomerId`
    * `Name` of type `CustomerName`
    * `Points` of type `Points`
    * `FrequentFlyerStatus` of type `FrequentFlyerStatus`

</div>

---

## Exercise 1.3 - create destination types

<div class="content">

* Measure type called `Km`
* `Distance` type of type `float<Km>`
* Type alias named `DestinationName` of type `string`
* Type alias named `DestinationId` of type `int`
* Record type `Destination` that has the following properties
    - `Id` of type `DestinationId`
    - `Name` of type `DestinationName`

</div>

---

## Exercise 1.4 - create ticket types

<div class="content">

* Measure type called `SEK`
* `Price` type of type `float<SEK>`
* Type alias named `DestinationName` of type `string`
* Type alias named `DestinationId` of type `int`
* Discriminated union type `TicketClass` with three cases
    - `Economy` with no field
    - `Business` with no field
    - `FirstClass` with no field

</div>

---

## Exercise 1.4 - create ticket types (cont.)

<div class="content">

* Record type `Ticket` that has the following properties
    - `CustomerId` of type `CustomerId`
    - `Price` of type `Price`
    - `TicketClass` of type `TicketClass`
    - `From` of type `Destination`
    - `To` of type `Destination`

</div>

***

<div class="intro-slide">

# Module 2

## Functions, operators, pattern matching, option and little bit of lists

</div>

---

## Functions

<div class="content">

Functions in F# are curried, that is, they only has <strong>1</strong> argument!

    let add x y = x + y
    let add x = fun y -> x + y

</div>

---

## Partial applications

<div class="content">

When you apply a function partially you get a new function back

    let add x y = x + y
    let add5 y = add 5 y
    let add5 = add 5        // fun y -> 5 + y

</div>

---

## Higher order functions

<div class="content">

You can both use functions as argument or return values of functions

* Returning a function


    let add x y = x + y
    let add5 = add 5

* Function as argument


    let add x y = x + y
    let add5 = add 5
    let addWith adder x = adder x
    addWith add5 5                  // 10

</div>

---

## Operators

<div class="content">

Functional operators are common in F#, and they are not that complicated as they seems. Two of the most common ones are the pipeline operator and function composition. You can easily define your own as well.

* `|>` pipeline operator


    (|>)    // ('a -> ('a -> 'b) -> 'b)

    let (|>>>) = fun x f -> f x

* `>>` function composition


    (>>)     // (('a -> 'b) -> ('b -> 'c) -> 'a -> 'c)

    let (>>>>) = fun f1 f2 a = f2 (f1 a)

</div>

---

## Options

<div class="content">

Built in type to use instead of null --> force explicit handling of missing values and a semantic meaning. It is implemented as a generic discriminated union.

    type Option<'T> = 
        | None
        | Some of 'T

</div>

---

## Pattern matching

<div class="content">

Can be used with any F# types:

    // Pattern match on tuple
    let myTuple = "tomas"*35
    let (name, age) = myTuple

    // Pattern match on discriminated union
    let maybeAdd x maybeY = 
        match maybeY with
        | None -> x
        | Some y -> x + y

    let isSome s = 
        match s with
        | None -> false
        | Some _ -> true // _ don't care match

</div>

---

## Pattern match (cont.)

<div class="content">

    // Pattern match on record type
    type Person = {Name: string; Age: int}
    let tomas = {Name = "tomas"; Age = 35}
    let {Name = name; Age = age} = tomas

</div>

---

## Lists, arrays and sequences

<div class="content">

* Lists 
    - immutable native F# lists


    let intList = [1;2;3]
    let intList = [1..3]

* Arrays
    - mutable arrays, more memory efficient


    let intList = [|1;2;3|]
    let intList = [|1..3|]

* Sequences (`IEnumerable`)
    - lazy
    - can be infinite


    let intSeq = seq { yield 1; yield 2}

</div>

---

## Pattern match on lists

<div class="content">

Of course you can pattern match on lists as well.


    let rec sumAll xs = 
        match xs with
        | [] -> 0                       // base case
        | [x::xs'] -> x + sumAll xs'    // recursive case

    // With tail recursion to avoid stack overflow
    let sumAll2 xs = 
        let rec inner xs acc = 
            match xs with
            | [] -> acc
            | [x::xs'] -> inner xs' (acc + x)
        inner xs 0

</div>

---

## Member functions

<div class="content">

You can also attach functions to types

    type Person =
        {
            Name: string
            Age: int
        }
        with 
            static member create name age = {Name = name; Age = age}
            member this.isAdult() = this.Age > 18

    let tomas = {Name = "tomas"; Age = 35}
    tomas.isAdult() // true

***

<div class="intro-slide">

# <strong>Exercises</strong> - Module 2

</div>

---

## Goal

<div class="content">

* Apply some structure to the project
* Basic functionality

</div>

---

## Exercise 2.1 - getting started

<div class="content">

* If you finished `module1` you can continue with the code
* If you did not finish `module1`, checkout `module2`
* Copy all the types from `Script.fsx` to `Types.fsx`

</div>

---

## Exercise 2.2 - Customer functions

<div class="content">

* Open the test project
* Comment out the first test in the `CustomerTests` module inside `TypesTest.fs`
* Implement a static `create` function on the `Customer` type
* Comment out the last two and implement the instance function `isGoldMember()`

</div>

---

## Exercise 2.3 - Destination functions

<div class="content">

* Comment out the first test in the `DestinationTests` module inside `TypesTest.fs`
* Implement a static `create` function on the `Destination` type

</div>

---

## Exercise 2.4 - Ticket functions

<div class="content">

* Comment out the first test in the `TicketTests` module inside `TypesTest.fs`
* Implement a static `create` function on the `Ticket` type

</div>

***

<div class="intro-slide">

# Module 3

## More options, lists

</div>

---

## Options level 2

<div class="content">

`Option.map` "lifts" a regular function a function that handles option

    let add5 x = 5 + x
    let maybeAdd5 = Option.map add5
    maybeAdd5 None          // None
    maybeAdd5 (Some 10)     // Some 15

`Option.bind` "lifts" a function that does not take an `Option` as input but returns it to a function that only deals with `Option` types

    let safeDiv10 y = if y = 0 then None else Some (10/y)
    let optSafeDiv10 = Option.bind safeDiv10
    optSafeDiv10 None       // None
    optSafeDiv10 (Some 2)   // Some 5

Available functions are in the `Option` namespace, use Visual Studio to see what is available.

</div>

---

## Lists level 2

<div class="content">

Most of the functions in the `List` module are also available in the `Seq` or `Array` module. Some of the `List` functions and their `LINQ` counterpart

<table>
    <tr>
        <th> F# </th>
        <th> C# </th>
    </tr>
    <tr>
        <td>List.filter</td>
        <td>.Where</td>
    </tr>
    <tr>
        <td>List.map</td>
        <td>.Select</td>
    </tr>
    <tr>
        <td>List.mapi</td>
        <td>.Select (with the index as well)</td>
    </tr>
    <tr>
        <td>List.fold</td>
        <td>.Aggregate</td>
    </tr>
    <tr>
        <td>List.find</td>
        <td>.First</td>
    </tr>
    <tr>
        <td>List.tryFind</td>
        <td>.FirstOrDefault</td>
    </tr>
    <tr>
        <td>List.collect</td>
        <td>.SelectMany</td>
    </tr>
    <tr>
        <td>List.exist</td>
        <td>.Any</td>
    </tr>
</table>

</div>

---

## List functions examples

<div class="content">

    let numbers = [ 1 .. 10 ]
    let evenNumbers = numbers |> List.filter (fun x -> x % 2 = 0)
    let sum = numbers |> List.fold (fun x y -> x + y) 0
    let double = numbers |> List.map ((*) 2)
    let doubleSum = 
        numbers
        |> List.map ((*) 2)
        |> List.fold (+) 0

Check the `List` module for more functions.

</div>

***

<div class="intro-slide">

# <strong>Exercises</strong> - Module 3

</div>

---

## Goal

<div class="content">

* Implement the domain logic for our ticketing system

</div>

---

## Exercise 3.1 - Conversion functions 

<div class="content">

* Comment out one test at a time from the `ConversionTests` module in `FunctionsTests.fs`
* Add a F# source file named `Functions.fs` in the `FSharpIntro` project below `Types.fs` (right click `Types.fs` and choose `Add below -> New Item...`)
* Add `open System` and `open Types` belove the top module definition
* Add a sub module `Conversion` in the new file


    module Conversion =

* Add the implementation that passes the test to this module, note that the functions need to be indented under this module
* (Hint on next page)

</div>

---

## Exercise 3.1 - Hint

<div class="content">

Implementation of the first function: 

    let priceToPoints (price: float<SEK>)= price * 1.<Point/SEK>

</div>

---

## Exercise 3.2 - Customer functions

<div class="content">

* Copy the file [Lists.fs](https://github.com/mastoj/FSharpIntro/blob/done/src/FSharpIntro/List.fs) and add it over `Types.fs`
* Add a new module in the `Functions.fs` file named `Customer`


    module Customer =

* Comment out one test at a time and make it pass
* When implementing `calculatePoints` you can use the `takeXOrAll` function in `Lists.fs`

</div>

---

## Exercise 3.3 - Ticket functions

<div class="content">

* Add a new module in the `Functions.fs` file named `Ticket


    module Ticket = 

* Comment out one test at a time and make it pass

***

<div class="intro-slide">

# Module 4

## Maps and basic OO

</div>

---

## Maps

<div class="content">

A `Map` can be thought of as a immutable `Dictionary`

</div>

---

## Maps - key functions

<div class="content">

    // Map.ofList
    [1,2; 2,1] |> Map.ofList // map [(1, 2); (2, 1)]
    
    // Map.toList
    [1,2; 2,1] |> Map.ofList |> Map.toList // [(1, 2); (2, 1)]

    // Map.add
    [1,2; 2,1] |> Map.ofList |> Map.add 3 4 // map [(1, 2); (2, 1); (3, 4)]

    // Map.find
    [1,2; 2,1] |> Map.ofList |> Map.find 1 // 2

    // Map.tryFind
    [1,2; 2,1] |> Map.ofList |> Map.tryFind 1 // Some 2

Use `Map.find` if you know that the value exists, otherwise use `tryFind` and deal with the missing key.

</div>

---

## Mutability in F#

<div class="content">

* In F# you want to avoid mutability, but some times it makes sense to bypass the default rules
* You can create mutable variables by using the keyword `mutable` and the `<-` operator to "re-bind" a variables.
* Try to do it at the ages or keep it local

</div>

---

## Mutability examples

<div class="content">

    let mutable x = 5
    x <- 6
    x = 5 // false
    x = 6 // true

    let mutable list = [1; 2]
    list <- 3::list
    list = [1; 2]       // false
    list = [3; 1; 2]    // true

</div>

---

## Creating classes

<div class="content">

The syntax is similar to records but you take arguments after the type name.

    type Person(name, age) = 
        let isAdult = age > 18
        do printfn "%s is adult: %A" name isAdult
        member this.Name = name // get & private set of Name
        member this.Age = age   // get & private set of age
        member this.IsAdult = isAdult
    
    let tomas = Person("tomas", 35)
    let tomas = new Person("tomas", 35)

</div>

---

## Interfaces

<div class="content">

An interface is a type with just abstract members.

    type Adder = 
        abstract member Add: int -> int -> int

    type AdderImpl() = 
        interface Adder with
            member this.Add x y = x + y

    let adder = AdderImpl()
    adder.Add 3 4 // ERROR
    (adder :> Adder).Add 3 4 // 7 -- (:>) is a type safe cast

</div>

---

### Object expressions

<div class="content">

It is possible to implement an interface in F# without implementing a class. It is done by a so called Object expression.

    type Adder = 
        abstract member Add: int -> int -> int

    let adder = 
        { new Adder
            with member this.Add x y = x + y }

    adder.Add 3 4 // 7, no need to cast since adder is of type Adder

</div>

***

<div class="intro-slide">

# <strong>Exercises</strong> - Module 4

</div>

---

## Goal

<div class="content">

* Implement in-memory data storage
* Learn more about lists, maps, OO and mutability

</div>

---

## Exercise 4.1 - Implement the in-memory store for destinations

<div class="content">

* Add the file `Data.fs` below the `Functions.fs` file
* Comment out one test a time and make it pass
* Key functions you might want to use
    - `List.mapi`
    - `List.collect`
    - `Map.ofList`
* There are hints to get you started on the next two slides

</div>

--- 

## Exercise 4.1 - Hints

<div class="content">

Some helper values:

    let private destinationList = 
        [ "Barcelona"; "Stockholm"; "Oslo" ] 
        |> List.mapi Destination.create
    let private distanceList = 
        [ (0,1,2789.2); (0,2,2141.16); (1,2,530.) ]
        |> List.collect (fun (index1, index2, distance) -> 
            let typedDistance = distance |> createDistance
            [
                (destinationList.[index1], destinationList.[index2]), typedDistance
                (destinationList.[index2], destinationList.[index1]), typedDistance
            ])
        |> Map.ofList

<div>

---

## Exercise 4.1 - More hints

<div class="content">


    let private priceList =
        [
            (0,1,700., 1500., 2000.)
            (0, 2, 600., 1300., 1800.)
            (1, 2, 400., 700., 1000. ) ]
        |> List.collect
            (fun (index1, index2, economyPrice, businessPrice, firstClassPrice) ->
                let [
                        typedEconomyPrice
                        typedBusinessPrice
                        typedFirstClassPrice ] =
                    [economyPrice; businessPrice; firstClassPrice]
                    |> List.map createPrice
                let prices = 
                    (typedEconomyPrice, typedBusinessPrice, typedFirstClassPrice
                [
                    (destinationList.[index1], destinationList.[index2]), prices
                    (destinationList.[index2], destinationList.[index1]), prices
                ])
        |> Map.ofList

</div>

---

## Exercise 4.2 - Implement the in-memory store for tickets

<div class="content">

* Comment out the `TicketsTests` and implement the functions it cover
* (This is probably not the best example of a good unit test)


</div>

---

## Exercise 4.3 - Implement the in-memory store for Customers

<div class="content">

* Comment out the tests in `CustomerTests` class one at a time and make it pass
* Note that we are using a `class` here so we can initialize the tests in the constructor block. That is the code after `do`.

</div>

---

## Exercise 4.4 - Implement DestinationService

<div class="content">

* Create a new file below `Data.fs` and name it `Services.fs`
* Add the class `DestinationService` to the file


    type DestinationService() = 

* Add member the following member functions that just call the functions in the `Data` module:


    member this.GetDestinations = // add correct function call here
    member this.GetDestination destinationId = // , here
    member this GetDistance ((destination1, destination2) as key) = // , here
    member this.GetPrice ((destination1, destination2) as key, ticketClass) // and here

</div>

---

## Exercise 4.5 - Implement TicketService

<div class="content">

* Add the class `TicketService`
* Add the members functions `GetTicketsForCustomer` and `SaveTicket`

</div>

---

## Exercise 4.6 - Implement CustomerService

<div class="content">

* Add the class `CustomerService`
* Add the member functions `GetCustomer`, `GetCustomers`, `AddCustomer` and `UpdateCustomer`

</div>

---

## Exercise 4.7 - Experiment with the services

<div class="content">

This time we will test that the code work through the `FSI`. Open up the file `Script1.fsx` and paste in the following to get you started:


    #load "Lists.fs"
    #load "Types.fs"
    #load "Functions.fs"
    #load "Data.fs"
    #load "Services.fs"

    open Services

    let customerService = new CustomerService()
    customerService.GetCustomers()
    customerService.AddCustomer "Tomas"
    customerService.GetCustomer 0

Run with `Alt+Enter` in Visual Studio.

</div>

***

<div class="intro-slide">

# Module 5

## Composition

</div>

--- 

## Composing applications

<div class="content">

When composing components it is done in mostly to different places:

* Compose types
* Compose functions

</div>

---

## Composing types

<div class="content">

Can be used to define external interface from an application

    type App =
        {
            getCustomers: unit -> Customer list
            addCustomer: CustomerName -> Customer option
            reserveTicket: ReserveTicketRequest -> (Customer * Ticket) option
            getTickets: CustomerId -> Ticket list
        }

</div>

---

## Composing functions

<div class="content">

Some times the function you want to expose does not match in type and/or structure with the domain function, or you need to combine multiple functions together.

    let createTicket request customer = 
        let ticket = Domain.createTicket customer request // Wrapping domain function
        match ticket with
        | None -> None
        | Some ticket -> Some (customer, ticket) // Creating new return value

    let reserveTicket ticketRequest : (Customer, Ticket) option =
        getCustomer customerId  // returns Customer option
        Option.bind (createTicket ticketRequest) // Return option
        ...

    let app = {
        reserveTicket: reserveTicket
    }

</div>

***

<div class="intro-slide">

# <strong>Exercises</strong> - Module 5

</div>

---

## Goal

<div class="content">

* Gluing the pieces together

</div>

---

## Exercise 5.1 - Create Application types

<div class="content">

* Add an `Application.fs` file below `Services.fs`
* Create a record type `ReserveTicketRequest` with the properties
    - `CustomerId: CustomerId`
    - `From: DestinationId`
    - `To: DestinationId`
    - `TicketClass: TicketClass`
* Create another record type `App` with the properties
    - `getCustomers: unit -> Customer list`
    - `addCustomer: CustomerName -> Customer option`
    - `reserveTicket: ReserveTicketRequest -> (Customer * Ticket) option`
    - `getTickets: CustomerId -> Ticket list`

</div>

---

## Exercise 5.2 - Add function to create the application

<div class="content">

* Add a `createApp` function in the bottom of the file (and keep it there always)
* `createApp` should have three arguments
    - `customerService: CustomerService`
    - `ticketService: TicketService`
    - `destinationService: TestinationService`

</div>

---

## Exercise 5.2 (cont.)

<div class="content">

* `createApp` should return a `App`


    {
        getCustomers = customerService.GetCustomers
        addCustomer = customerService.AddCustomer
        reserveTicket = 
            (reserveTicket customerService ticketService destinationService)
        getTickets = ticketService.GetTicketsForCustomer
    }

* Implement a dummy function for `reserveTicket`
* Try out the `createApp` with the script file

</div>

---

## Exercise 5.3 - Implement reserveTicket

<div class="content">

* Add the `reserveTicket` function above `createApp`


    let reserveTicket 
        (customerService: CustomerService) 
        (ticketService: TicketService) 
        (destinationService: DestinationService) reserveTicketRequest = 


* Implement the following functions in the body of `reserveTicket`, some functions might shadow functions in `Functions.fs`:


    let tryUpgradeTicketForCustomer (customer, ticket) 
        : (Customer * Ticket) option =

    let createTicket 
        reserveTicketRequest
        ((customer: Customer), fromDestination, toDestination) 
        : (Customer * Ticket) option = 

</div>

--- 

## Exercise 5.3 - Implement reserveTicket (cont.)

<div class="content">

    let getFromDestionation reserveTicketRequest customer 
        : (Customer * Destination) option = 

    let getToDestination reserveTicketRequest (customer, from) 
        : (Customer * Destination * Destination) option = 

    let updateCustomer ((customer:Customer), ticket)
        : (Customer * Ticket) option = 

    let saveTicket (customer, ticket) 
        : (Customer * Ticket) option = 

    let saveCustomer (customer, ticket) 
        : (Customer * Ticket) option = 

</div>

---

## Exercise 5.4 - Bind the helper functions together

<div class="content">

* Use `Option.bind` and do the following:
    1. get customer
    2. get from destination
    3. get to destination
    4. create ticket
    5. try upgrade ticket for customer
    6. save ticket
    7. update customer
    8. save customer

* Hint on next slide to get you started

</div>

---

## Exercise 5.4 - Hint

<div class="content">

    let reserveTicket ... = 

        // ... helper functions

        customerService.GetCustomer reserveTicketRequest.CustomerId
        Option.bind (getFromDestionation reserveTicketRequest)
        // add the rest here

</div>

--- 

## Exercise 5.5 - Create the console application

<div class="content">

* This is mainly boiler plate code so replace `Program.fs` with the one [here](https://github.com/mastoj/FSharpIntro/blob/done/src/FSharpIntro/Program.fs).
* This is the first function with recursion, the `rec` keyword is used
* Try to understand the code (reading code is important)
* Start the application and play around with it

</div>

***

