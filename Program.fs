open System.Security.AccessControl
open day19_try3

open day19_try3.Base
open day19_try3.Solver
open day19_try3.PreCalced
open day19_try3.Track
open day19_try3.Cache

// let input = Input.read "/tmp/aoc/input.19.t"
let input = Input.read "/tmp/aoc/input"

input |> List.map (printfn "%A")

solve (Time.init 32) input[0]

// let testExample1Cutoffs () =
//    PreCalced(input[0])

// wrong answer for part 2 - need to check cutoffs against the examples (see example file)





// == Minute 1 ==


let testAt
    (minute: int)
    (resources: Resources)
    (production: Production)
    (build: Option<Robot>)
    (lastBuild: Option<Robot>)
    (lastRes: Resources)
    =
    let precalced: PreCalced = PreCalced(input[0])
    let time = Time(minute, 32 - minute, "")
    let track: Track = Track(time, production, resources, build, lastBuild, lastRes)
    let cache: Cache = Cache.init precalced
    let shouldAbort = CutOffs.shouldAbort track cache precalced
    printfn $"ShouldAbort (minute: {minute} = {shouldAbort}"

let ore = Some(input[0].Ore)
let clay = Some(input[0].Clay)
let obs = Some(input[0].Obsidian)
let geo = Some(input[0].Geode)

testAt 1 (Resources(1, 0, 0, 0)) (Production(1, 0, 0, 0)) None None (Resources(0, 0, 0, 0))
// [1 0 0 0] -> [1 0 0 0]

// == Minute 2 ==
testAt 2 (Resources(2, 0, 0, 0)) (Production(1, 0, 0, 0)) None None (Resources(1, 0, 0, 0))
// [1 0 0 0] -> [2 0 0 0]
// == Minute 3 ==
testAt 3 (Resources(3, 0, 0, 0)) (Production(1, 0, 0, 0)) None None (Resources(2, 0, 0, 0))
// [1 0 0 0] -> [3 0 0 0]

// == Minute 4 ==
// [1 0 0 0] -> [4 0 0 0]
testAt 4 (Resources(4, 0, 0, 0)) (Production(1, 0, 0, 0)) ore None (Resources(3, 0, 0, 0))
// == Minute 5 ==
// [1 0 0 0] -> [0 0 0 0]+ORE + [1 - - -]   o
testAt 5 (Resources(1, 0, 0, 0)) (Production(1, 0, 0, 0)) None ore (Resources(4, 0, 0, 0))
// == Minute 6 ==
// [1 0 0 0] -> [3 0 0 0]
testAt 6 (Resources(1, 0, 0, 0)) (Production(2, 0, 0, 0)) None ore (Resources(1, 0, 0, 0))
// == Minute 7 ==
// [2 0 0 0] -> [1 0 0 0]+CLAY + [3 0 0 0]  oc
testAt 7 (Resources(3, 0, 0, 0)) (Production(2, 0, 0, 0)) clay None (Resources(1, 0, 0, 0))
// == Minute 8 ==
// [3 0 0 0] -> [1 0 0 0]+CLAY + [3 1 0 0]  occ
testAt 8 (Resources(3, 1, 0, 0)) (Production(2, 1, 0, 0)) clay clay (Resources(3, 0, 0, 0))
// == Minute 9 ==
// [3 1 0 0] -> [1 1 0 0]+CLAY + [3 3 0 0]  oc3
testAt 9 (Resources(3, 3, 0, 0)) (Production(2, 2, 0, 0)) clay clay (Resources(3, 1, 0, 0))
// == Minute 10 ==
// [3 3 0 0] -> [1 3 0 0]+CLAY + [3 6 0 0]  oc4
testAt 10 (Resources(3, 3, 0, 0)) (Production(2, 3, 0, 0)) clay clay (Resources(3, 1, 0, 0))
// == Minute 11 ==
// [3 6 0 0] -> [1 6 0 0]+CLAY + [3 10 0 0] oc5
// == Minute 12 ==
// [3 10 0 0] -> [1 10 0 0]+CLAY + [3 15 0 0] oc6
// == Minute 13 ==
// [3 15 0 0] -> [1 15 0 0]+CLAY + [3 21 0 0] oc7
// == Minute 14 ==
// [3 21 0 0] -> [0 7 0 0+OBS + [2 14 0 0] oc7o
// == Minute 15 ==
// [2 14 0 0] -> [4 21 1 0]
// == Minute 16 ==
// [4 21 1 0] -> [1 7 1 0]+OBS + [3 14 2] +oc7o2
// == Minute 17 ==
// [3 14 2 0] -> [0 0 2 0]+OBS + [2 7 4 0] oc7o3
testAt 17 (Resources(3, 14, 2, 0)) (Production(2, 7, 2, 0)) obs obs (Resources(3, 14, 2, 0))

// == Minute 18 ==
// [2 7 4 0] -> [4 14 7 0]
// == Minute 19 ==
// [4 14 7 0] -> [1 0 7 0]+OBS + [3 7 10 0] oc7o4
// == Minute 20 ==
// [3 7 10 0] -> [1 7 3 0]+GEO + [3 14 7 0] oc7o4g
// == Minute 21 ==
// [3 14 7 0] -> [0 0 7 0]+OBS + [2 7 11 1] oc7o5g
// == Minute 22 ==
// [2 7 11 5] -> [0 7 4 5]+GEO + [2 14 9 2] oc7o5g2
// == Minute 23 ==
// [2 14 9 2] -> [0 14 2 2]+GEO + [2 21 7 4] oc7o5g3
// == Minute 24 ==
// [2 21 7 4] -> [0 21 0 4]+GEO + [2 28 5 7] oc7o5g4
// == Minute 25 ==
// [2 28 5 7] -> [4 35 10 11]
// == Minute 26 ==
// [4 35 10 11] -> [2 35 3 0]+GEO + [4 42 8 15] oc7o5g5
// == Minute 27 ==
// [4 42 8 15] -> [2 42 1 15]+GEO + [4 49 6 20] oc7o5g6
// == Minute 28 ==
// [4 49 6 20] -> [6 56 11 26]
// == Minute 29 ==
// [6 56 11 26] -> [4 56 4 26]+GEO + [6 63 9 32] oc7o5g7
// == Minute 30 ==
// [6 63 9 32] -> [4 63 2 32]+GEO + [6 70 7 39] oc7o5g8
// == Minute 31 ==
// [6 70 7 39] -> [4 70 0 39]+GEO + [6 77 5 47] oc7o5g9
testAt 31 (Resources(6, 70, 7, 39)) (Production(2, 7, 5, 8)) geo geo (Resources(6, 63, 9, 32))
// == Minute 32 ==
// 8 84 10 56] -> [0 0 0 0]

let v1 = (solve (Time.init 32) input[0]).Best
let v2 = (solve (Time.init 32) input[1]).Best

let v3 = (solve (Time.init 32) input[2]).Best

let answer2 = v1 * v2 * v3
printfn $"Answer 2: {answer2}"