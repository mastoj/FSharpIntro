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
* Module 3 - More lists, maps
* Module 4 - Basic OO
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

    let x = 5                   // bind the value 5 to the variable x
    let add = fun x y -> x + y  // bind the function x + y to add
    x = x + 1                   // false, here it is equal and not bind

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
    * `Id` of type `DestinationId`
    * `Name` of type `DestinationName`

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

## More lists, maps

</div>

***

<div class="intro-slide">

# <strong>Exercises</strong> - Module 3

</div>

***

<div class="intro-slide">

# Module 4

## Basic OO

</div>

***

<div class="intro-slide">

# <strong>Exercises</strong> - Module 4

</div>

***

<div class="intro-slide">

# Module 5

## Composition

</div>

***

<div class="intro-slide">

# <strong>Exercises</strong> - Module 5

</div>

***








***

<div class="columns">

<div class="col-1-2">

- Generates [reveal.js](http://lab.hakim.se/reveal-js/#/) presentation from [markdown](http://daringfireball.net/projects/markdown/)
- Utilizes [FSharp.Formatting](https://github.com/tpetricek/FSharp.Formatting) for markdown parsing
- Get it from [http://fsprojects.github.io/FsReveal/](http://fsprojects.github.io/FsReveal/)

</div>

<div class="col-1-4">

![FsReveal](images/logo.png)

</div>

<div class="col-1-4" style="background-color: red;">

do I have this in a block as

</div>

***

### Reveal.js

- A framework for easily creating beautiful presentations using HTML.


> **Atwood's Law**: any application that can be written in JavaScript, will eventually be written in JavaScript.

***

### FSharp.Formatting

- F# tools for generating documentation (Markdown processor and F# code formatter).
- It parses markdown and F# script file and generates HTML or PDF.
- Code syntax highlighting support.
- It also evaluates your F# code and produce tooltips.

***

### Syntax Highlighting

#### F# (with tooltips)

    let a = 5
    let factorial x = [1..x] |> List.reduce (*)
    let c = factorial a

---

#### C#

    [lang=cs]
    using System;

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, world!");
        }
    }

---

#### JavaScript

    [lang=js]
    function copyWithEvaluation(iElem, elem) {
        return function (obj) {
            var newObj = {};
            for (var p in obj) {
                var v = obj[p];
                if (typeof v === "function") {
                    v = v(iElem, elem);
                }
                newObj[p] = v;
            }
            if (!newObj.exactTiming) {
                newObj.delay += exports._libraryDelay;
            }
            return newObj;
        };
    }


---

#### Haskell
 
    [lang=haskell]
    recur_count k = 1 : 1 : 
        zipWith recurAdd (recur_count k) (tail (recur_count k))
            where recurAdd x y = k * x + y

    main = do
      argv <- getArgs
      inputFile <- openFile (head argv) ReadMode
      line <- hGetLine inputFile
      let [n,k] = map read (words line)
      printf "%d\n" ((recur_count k) !! (n-1))

*code from [NashFP/rosalind](https://github.com/NashFP/rosalind/blob/master/mark_wutka%2Bhaskell/FIB/fib_ziplist.hs)*

---

### SQL

    [lang=sql]
    select *
    from
    (select 1 as Id union all select 2 union all select 3) as X
    where Id in (@Ids1, @Ids2, @Ids3)

*sql from [Dapper](https://code.google.com/p/dapper-dot-net/)*

---

### Paket

    [lang=paket]
    source https://nuget.org/api/v2

    nuget Castle.Windsor-log4net >= 3.2
    nuget NUnit
    
    github forki/FsUnit FsUnit.fs
      
---

### C/AL

    [lang=cal]
    PROCEDURE FizzBuzz(n : Integer) r_Text : Text[1024];
    VAR
      l_Text : Text[1024];
    BEGIN
      r_Text := '';
      l_Text := FORMAT(n);

      IF (n MOD 3 = 0) OR (STRPOS(l_Text,'3') > 0) THEN
        r_Text := 'Fizz';
      IF (n MOD 5 = 0) OR (STRPOS(l_Text,'5') > 0) THEN
        r_Text := r_Text + 'Buzz';
      IF r_Text = '' THEN
        r_Text := l_Text;
    END;

***

**Bayes' Rule in LaTeX**

$ \Pr(A|B)=\frac{\Pr(B|A)\Pr(A)}{\Pr(B|A)\Pr(A)+\Pr(B|\neg A)\Pr(\neg A)} $

***

### The Reality of a Developer's Life 

**When I show my boss that I've fixed a bug:**
  
![When I show my boss that I've fixed a bug](http://www.topito.com/wp-content/uploads/2013/01/code-07.gif)
  
**When your regular expression returns what you expect:**
  
![When your regular expression returns what you expect](http://www.topito.com/wp-content/uploads/2013/01/code-03.gif)
  
*from [The Reality of a Developer's Life - in GIFs, Of Course](http://server.dzone.com/articles/reality-developers-life-gifs)*

