module List

let findOrDefault predicate defaultValue list =
    match list |> List.tryFind predicate with
    | None -> defaultValue
    | Some x -> x

let takeXOrAll x list = 
    if list |> List.length >= x then list |> List.take x
    else list
