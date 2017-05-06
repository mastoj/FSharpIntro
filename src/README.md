# Extra exercise - add suave

Suave is a idiomatic web framework for F# that is very easy to work with when you get used to it. How you achieve your result will not be described in detail as in the actual workshop, but here are some pointer.

* Start from the `done` branch
* Add web project
* Remove `FSharp.Core` reference and add it from nuget instead, make sure you have the same in all projects 
* Install suave
* Start coding

References:

* A presentation I did from NDC in 2016: https://www.slideshare.net/mastoj/functional-webapplicaationsusingf-sharpandsuave-60952876, https://vimeo.com/171704578
* https://suave.io

The code in this repo is a sample solution adding suave, so make sure you start from `done` and peak here if you're stuck.

## Issues

* For some reason Suave 2.x doesn't work with .NET 4.5.2, so make sure you are targeting at least 4.6.1 in the web projects.
* For simplicity I would use json.net for json handling. 

    let toJson o = JsonConvert.SerializeObject o
    let fromJson<'T> str = JsonConvert.DeserializeObject<'T>(str)

