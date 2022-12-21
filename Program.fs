open day19_try3

open day19_try3.Base
open day19_try3.Solver

let input = Input.read "/tmp/aoc/input.19.t"

input |> List.map (printfn "%A")

solve (Time.init 18) input[0] 