module day19_try3.CutOffs

open System.Diagnostics.Contracts
open day19_try3.Base
open day19_try3.Track
open day19_try3.Cache
open day19_try3.PreCalced

type CutOff(track: Track, cache: Cache, preCalculed: PreCalced) =

    let lacksResourcesFor (robot: Robot) (resources: Resources) =
        match robot.Material with
        | Ore -> robot.Cost.Ore > resources.Ore
        | Clay -> robot.Cost.Ore > resources.Ore
        | Obsidian -> (robot.Cost.Ore > resources.Ore) || (robot.Cost.Clay > resources.Clay)
        | Geode -> (robot.Cost.Ore > resources.Ore) || (robot.Cost.Obs > resources.Obs)

    member this.OutOfTime = track.Time.OutOfTime

    member this.CantBuild() =
        match track.Build with
        | None -> false
        | Some (robot) -> lacksResourcesFor robot track.Resources

    member this.CouldHaveBuiltButSlept() =
        if track.LastBuild.IsSome || track.Build.IsNone then
            false
        else
            let robot = track.Build |> Option.get
            lacksResourcesFor robot track.lastResources |> not
    // could have built it last round
    member this.OverProduction() =
        if track.Production.Ore > preCalculed.MaxNeeded Ore then
            true
        elif track.Production.Clay > preCalculed.MaxNeeded Clay then
            true
        elif track.Production.Obs > preCalculed.MaxNeeded Obsidian then
            true
        else
            false

    member this.SeenBetter() =
        if cache.isWorse track then
            printfn "Seen better"
            true
        else
            false

    member this.CouldHaveBuiltAny() =
        if track.Build.IsSome then
            false
        elif track.Resources.Ore < preCalculed.MaxNeeded Ore then
            false
        elif track.Resources.Clay < preCalculed.MaxNeeded Clay then
            false
        elif track.Resources.Obs < preCalculed.MaxNeeded Obsidian then
            false
        else
            true // could have built anything, but built nothing

let shouldAbort (track: Track) (cache: Cache) (preCalculated: PreCalced) : bool =
    let cutOff = CutOff(track, cache, preCalculated)

    if cutOff.OutOfTime then true
    elif cutOff.CantBuild() then true
    elif cutOff.CouldHaveBuiltButSlept() then true
    elif cutOff.OverProduction() then true
    elif cutOff.CouldHaveBuiltAny() then true
    elif cutOff.SeenBetter() then true
    else false
