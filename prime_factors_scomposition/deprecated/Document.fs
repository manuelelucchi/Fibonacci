module Document

open System
open Core

type DocumentAction =
    | Fibonacci
    | PowerOf
    | PowerOfExcept
    | PowerOfOnly
    | MultiPowerOf
    | CompareDistanceSub
    | CompareDistanceDiv

let DocumentValues: array<DocumentAction * Object> =
    [| (Fibonacci, upcast (100))
       (PowerOf, upcast (100, 2))
       (PowerOf, upcast (1000, 2))
       (PowerOf, upcast (100, 3))
       (PowerOf, upcast (100, 5))
       (PowerOf, upcast (10000, 23))
       (PowerOf, upcast (100, 4))
       (PowerOfExcept, upcast (100, 2, 3))
       (PowerOfOnly, upcast (100, 2, 3))
       (MultiPowerOf, upcast (100, [| 2; 4 |]))
       (MultiPowerOf, upcast (100, [| 2; 3 |]))
       (CompareDistanceSub, upcast (1000, 2, 4, 1))
       (CompareDistanceSub, upcast (1000, 3, 9, 1))
       (CompareDistanceSub, upcast (600, 3, 9, 2))
       (CompareDistanceSub, upcast (500, 2, 3, 1))
       (CompareDistanceDiv, upcast (1000, 2, 4, 1))
       (CompareDistanceDiv, upcast (1000, 3, 9, 1))
       (CompareDistanceDiv, upcast (600, 3, 9, 2))
       (CompareDistanceDiv, upcast (500, 2, 3, 1)) |]

let Path = "../Assets"

let Generate =
    for value in DocumentValues do
        let (t, args) = value
        match t with
        | Fibonacci -> FibonacciSave (downcast args: int) Path
        | PowerOf ->
            let (n, power) = (downcast args: int * int)
            PowerOfSave n power Path
        | PowerOfExcept ->
            let (n, power, except) = (downcast args: int * int * int)
            PowerOfExceptSave n power except Path
        | PowerOfOnly ->
            let (n, power, only) = (downcast args: int * int * int)
            PowerOfOnlySave n power only Path
        | MultiPowerOf ->
            let (n, powers) = (downcast args: int * array<int>)
            MultiPowerOfSave n powers Path
        | CompareDistanceSub ->
            let (n, x1, x2, exp) = (downcast args: int * int * int * int)
            CompareDistanceSave CompareType.Sub n x1 x2 exp Path
        | CompareDistanceDiv ->
            let (n, x1, x2, exp) = (downcast args: int * int * int * int)
            CompareDistanceSave CompareType.Div n x1 x2 exp Path
        | _ -> raise (InvalidOperationException("Tipo non riconosciuto"))

[<EntryPoint>]
let main argv =
    Generate
    Console.ReadKey |> ignore
    0 // return an integer exit code
