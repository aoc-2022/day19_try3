module day19_try3.Track

open day19_try3.Base

type Track(time: Time, production: Production, resources: Resources, build:Option<Robot>, lastBuild:Option<Robot>, lastRes:Resources) =
    member this.Time = time
    member this.Production = production
    member this.Resources = resources
    member this.Build = build
    member this.LastBuild = lastBuild
    member this.lastResources = lastRes 
    
    member this.Tick(newBuild: Option<Robot>) = 
        let time = time.Tick ()
        let prevResources = resources 
        let resources = resources.Add production
        let (production,resources) = match build with
        | None ->
            production,resources
        | Some(robot) ->
            let production = production.Add robot.Production
            let resources = resources.Remove robot.Cost
            (production, resources)
        Track(time,production,resources,newBuild,build,prevResources)
        
    override this.ToString() =
        let building (build:Option<Robot>) =
            match build with
            | None -> "waiting"
            | Some(robot) -> $"building {robot.Material}"
        let buildNow = building build
        let builtLast = building lastBuild
        
        $"Track({time.Indent} {time} {production} {resources} {buildNow} prev={builtLast})"

    static member initial (time: Time) =
        Track(time, Production.initial, Resources.empty,None,None,Resources.empty)