module Core

open FSharp.Charting
open FSharp.Charting.ChartTypes
open System

(* Private Members *)
let private FibonacciTitle = "Fibonacci Sequence"
let private FibonacciLegend = "Legend"
let private FibonacciXTitle = "N"
let private FibonacciYTitle = "Fibonacci(N)"

let private PowerOfTitle e =
    sprintf "Power of %i in Fibonacci Numbers" e

let private PowerOfLegend = ""
let private PowerOfXTitle = "N"
let private PowerOfYTitle e = sprintf "Power of %i in N" e

let private PowerOfOnlyTitle e =
    sprintf "Power of %i in Fibonacci Numbers" e

let private PowerOfOnlyLegend = ""
let private PowerOfOnlyXTitle = "N"
let private PowerOfOnlyYTitle e = sprintf "Power of %i in N" e

let private PowerOfExceptTitle e =
    sprintf "Power of %i in Fibonacci Numbers" e

let private PowerOfExceptLegend = ""
let private PowerOfExceptXTitle = "N"
let private PowerOfExceptYTitle e = sprintf "Power of %i in N" e
let private MultiPowerOfTitle = ""
let private MultiPowerOfLegend = ""
let private MultiPowerOfXTitle = ""
let private MultiPowerOfYTitle = ""
let private SubCompareTitle = ""
let private SubCompareLegend = ""
let private SubCompareXTitle = ""
let private SubCompareYTitle = ""
let private DivCompareTitle = ""
let private DivCompareLegend = ""
let private DivCompareXTitle = ""
let private DivCompareYTitle = ""

(* Helper Types *)

type CompareType =
    | Sub = 0
    | Div = 1

(* Helper Functions *)

let SeqToString (seq: seq<'T>) =
    Seq.fold (fun str1 str2 -> str1 + "_" + string str2) "" seq

let FibonacciSequence (n: int) =
    Seq.unfold (fun (a, b) -> Some(a + b, (b, a + b))) (bigint 0, bigint 1)
    |> Seq.truncate n

let FibonacciNumber (n: int): bigint = FibonacciSequence n |> Seq.last

let GetPowerOf (n: bigint) (power: bigint): bigint =
    let mutable ret = bigint 0
    let mutable br = false
    let mutable count = bigint 0
    let mutable nCopy = n
    if nCopy = bigint 0 then
        ret <- bigint 0
    else if power = bigint 1 then
        raise
            (InvalidOperationException
                ("Cannot get the power of "
                 + power.ToString()
                 + " in "
                 + n.ToString()))
        ret <- bigint 0

    while not br do
        if nCopy % power = bigint 0 then
            count <- count + bigint 1
            nCopy <- nCopy / power
        else
            br <- true
    count

(* Private Functions *)

let private FibonacciCommon (n: int) =
    Seq.mapi (fun x y -> (string x, string y)) (FibonacciSequence n)
    |> Chart.Line
    |> Chart.WithTitle FibonacciTitle
    |> Chart.WithLegend(Title = FibonacciLegend)
    |> Chart.WithXAxis(Title = FibonacciXTitle)
    |> Chart.WithYAxis(Title = FibonacciYTitle)

let private PowerOfCommon (n: int) (power: int) =
    Seq.mapi (fun x y -> (string x, string (GetPowerOf y (bigint power)))) (FibonacciSequence n)
    |> Chart.Line
    |> Chart.WithTitle(PowerOfTitle power)
    |> Chart.WithLegend(Title = PowerOfLegend)
    |> Chart.WithXAxis(Title = PowerOfXTitle)
    |> Chart.WithYAxis(Title = (PowerOfYTitle power))

let private PowerOfExceptCommon (n: int) (power: int) (except: int) =
    Seq.mapi (fun x y -> (x, GetPowerOf y (bigint power))) (FibonacciSequence n)
    |> Seq.filter (fun (x, y) -> x % except <> 0)
    |> Seq.map (fun (x, y) -> (string x, string y))
    |> Chart.Line
    |> Chart.WithTitle(PowerOfExceptTitle power)
    |> Chart.WithLegend(Title = PowerOfExceptLegend)
    |> Chart.WithXAxis(Title = PowerOfExceptXTitle)
    |> Chart.WithYAxis(Title = (PowerOfExceptYTitle power))

let private PowerOfOnlyCommon (n: int) (power: int) (only: int) =
    Seq.mapi (fun x y -> (x, GetPowerOf y (bigint power))) (FibonacciSequence n)
    |> Seq.filter (fun (x, y) -> x % only = 0)
    |> Seq.map (fun (x, y) -> (string x, string y))
    |> Chart.Line
    |> Chart.WithTitle(PowerOfOnlyTitle power)
    |> Chart.WithLegend(Title = PowerOfOnlyLegend)
    |> Chart.WithXAxis(Title = PowerOfOnlyXTitle)
    |> Chart.WithYAxis(Title = (PowerOfOnlyYTitle power))

let private MultiPowerOfCommon (n: int) (power: seq<int>) =
    power
    |> Seq.map (fun x -> bigint x)
    |> Seq.map (fun p ->
        ((FibonacciSequence n)
         |> Seq.mapi (fun x y -> (string x, string (GetPowerOf y p))))
        |> Chart.Line)
    |> Chart.Combine
    |> Chart.WithTitle MultiPowerOfTitle
    |> Chart.WithLegend(Title = MultiPowerOfLegend)
    |> Chart.WithXAxis(Title = MultiPowerOfXTitle)
    |> Chart.WithYAxis(Title = MultiPowerOfYTitle)

let private CompareDistanceCommon (compareType: CompareType) (n: int) (power1: int) (power2: int) (exponent: int) =
    let op x1 x2 =
        match compareType with
        | CompareType.Sub -> float x1 - float x2
        | CompareType.Div -> float x1 / float x2
        | _ -> raise (InvalidOperationException("Valore sconosciuto"))

    let Y1 =
        FibonacciSequence(n)
        |> Seq.mapi (fun i y -> (i, GetPowerOf y (bigint power1)))
        |> Seq.filter (fun (_, y) -> y = (bigint exponent))
        |> Seq.map fst

    let Y2 =
        FibonacciSequence(n)
        |> Seq.mapi (fun i y -> (i, GetPowerOf y (bigint power2)))
        |> Seq.filter (fun (_, y) -> y = (bigint exponent))
        |> Seq.map fst

    Seq.mapi2 (fun i y1 y2 -> (string i, string (op y1 y2))) Y1 Y2
    |> Chart.Line
    |> (fun chart ->
        match compareType with
        | CompareType.Sub ->
            (chart
             |> Chart.WithTitle SubCompareTitle
             |> Chart.WithLegend(Title = SubCompareLegend)
             |> Chart.WithXAxis(Title = SubCompareXTitle)
             |> Chart.WithYAxis(Title = SubCompareYTitle))
        | CompareType.Div ->
            (chart
             |> Chart.WithTitle DivCompareTitle
             |> Chart.WithLegend(Title = DivCompareLegend)
             |> Chart.WithXAxis(Title = DivCompareXTitle)
             |> Chart.WithYAxis(Title = DivCompareYTitle))
        | _ -> raise (InvalidOperationException("Valore sconosciuto")))


(* Public Members *)

(* Fibonacci *)

let FibonacciPlot (n: int) = FibonacciCommon n |> Chart.Show

let FibonacciSave (n: int) (savePath: string) =
    FibonacciCommon n
    |> Chart.Save(sprintf "%s//Fibonacci_In_%i.png" savePath n)

(* Power Of *)

let PowerOfPlot (n: int) (power: int) = PowerOfCommon n power |> Chart.Show

let PowerOfSave (n: int) (power: int) (savePath: string) =
    PowerOfCommon n power
    |> Chart.Save(sprintf "%s//PowerOf_%i_In_%i.png" savePath power n)

(* Power Of Except*)

let PowerOfExceptPlot (n: int) (power: int) (except: int) =
    PowerOfExceptCommon n power except |> Chart.Show

let PowerOfExceptSave (n: int) (power: int) (except: int) (savePath: string) =
    PowerOfExceptCommon n power except
    |> Chart.Save(sprintf "%s//PowerOf_%i_Except_%i_In_%i.png" savePath power except n)

(* Power Of Only*)

let PowerOfOnlyPlot (n: int) (power: int) (only: int) =
    PowerOfOnlyCommon n power only |> Chart.Show

let PowerOfOnlySave (n: int) (power: int) (only: int) (savePath: string) =
    PowerOfOnlyCommon n power only
    |> Chart.Save(sprintf "%s//PowerOf_%i_Only_%i_In_%i.png" savePath power only n)

(* Multi Power Of *)

let MultiPowerOfPlot (n: int) (power: seq<int>) = MultiPowerOfCommon n power |> Chart.Show

let MultiPowerOfSave (n: int) (power: seq<int>) (savePath: string) =
    MultiPowerOfCommon n power
    |> Chart.Save(sprintf "%s//MultiPowerOf%s_In_%i.png" savePath (SeqToString power) n)

(* Compare Distance *)

let CompareDistancePlot (compareType: CompareType) (n: int) (power1: int) (power2: int) (exponent: int) =
    CompareDistanceCommon compareType n power1 power2 exponent
    |> Chart.Show

let CompareDistanceSave (compareType: CompareType)
                        (n: int)
                        (power1: int)
                        (power2: int)
                        (exponent: int)
                        (savePath: string)
                        =
    CompareDistanceCommon compareType n power1 power2 exponent
    |> Chart.Save
        (sprintf
            "%s//%s_%i_%i_DistanceFor_%i_In_%i.png"
             savePath
             (if compareType = CompareType.Sub then "Sub" else "Div")
             power1
             power2
             exponent
             n)
