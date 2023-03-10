module day19_try3.CutOffs

open System.Diagnostics.Contracts
open day19_try3.Base
open day19_try3.Track
open day19_try3.Cache
open day19_try3.PreCalced

type CutOff(track: Track, cache: Cache, preCalculed: PreCalced) =

    let lacksResourcesFor (robot: Robot) (resources: Resources) =
        // printfn $"lacksResourcesFor {robot} {resources}"
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
        let maxOre = preCalculed.MaxNeeded Ore
        let ore = track.Resources.Ore
        let maxClay = preCalculed.MaxNeeded Clay
        let clay = track.Resources.Clay
        let maxObs = preCalculed.MaxNeeded Obsidian
        let obs = track.Resources.Obs
        let t = track.Time.Left + 2

        if
            track.Production.Ore + ((ore - maxOre) / t) >= maxOre
            && track.Build = Some(preCalculed.BluePrint.Ore)
        then
            true
        elif
            track.Production.Clay + ((clay - maxClay) / t) >= maxClay
            && track.Build = Some(preCalculed.BluePrint.Clay)
        then
            true
        elif
            track.Production.Obs + ((obs - maxObs) / t) >= maxObs
            && track.Build = Some(preCalculed.BluePrint.Obsidian)
        then
            true
        else
            false

    member this.SeenBetter() =
        if cache.isWorse track then
            // printfn "Seen better"
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

    if cutOff.OutOfTime then
        // printfn "OUT OF TIME"
        true
    elif cutOff.CantBuild() then
        // printfn "CANT BUILD"
        true
    elif cutOff.CouldHaveBuiltButSlept() then
        // printfn "COULD HAVE BUILT PREV STEP"
        true
    elif cutOff.OverProduction() then
        // printfn "OVER PRODUCTION"
        true
    elif cutOff.CouldHaveBuiltAny() then
        // printfn "COULD HAVE BUILT ANY"
        true
    elif cutOff.SeenBetter() then
        // printfn "SEEN BETTER"
        true
    else
        false
