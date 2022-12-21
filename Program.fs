open day19_try3

open day19_try3.Base
open day19_try3.Solver
open day19_try3.PreCalced
open day19_try3.Track
open day19_try3.Cache 

let input = Input.read "/tmp/aoc/input.19.t"

input |> List.map (printfn "%A")

solve (Time.init 24) input[0]

// let testExample1Cutoffs () =
//    PreCalced(input[0])

// wrong answer for part 2 - need to check cutoffs against the examples (see example file)
    
    
