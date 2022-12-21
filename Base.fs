module day19_try3.Base

open System.Net.Http.Headers

type Material =
    | Ore
    | Clay
    | Obsidian
    | Geode

type Time(passed: int, left: int, prefix: string) =
    member this.Passed = passed
    member this.Left = left
    member this.Indent = " " |> String.replicate passed 

    member this.Tick() =
        let passed = passed + 1
        let left = left - 1
        let prefix = " " |> String.replicate passed
        Time(passed, left, prefix)
        
    member this.OutOfTime = left = 0 

    override this.ToString() = $"T{left}"

    static member init(left: int) = Time(0, left, "")



type Production(ore: int, clay: int, obs: int, geo: int) =
    member this.Ore = ore
    member this.Clay = clay
    member this.Obs = obs
    member this.Geo = geo
    member this.AllRobots = [geo;obs;clay;ore]
    
    member this.Add (other:Production) =
        Production (ore + other.Ore, clay + other.Clay, obs + other.Obs, geo + other.Geo)
    override this.ToString() = $"Production[{ore} {clay} {obs} {geo}]"
    
    static member initial = Production(1,0,0,0)

type Resources(ore: int, clay: int, obs: int, geo: int) =
    member this.Ore = ore
    member this.Clay = clay
    member this.Obs = obs
    member this.Geo = geo
    member this.Remove (other:Resources) =
        Resources (ore - other.Ore, clay - other.Clay, obs - other.Obs, geo - other.Geo) 
    member this.Add (production:Production) =
        Resources (ore + production.Ore, clay + production.Clay, obs + production.Obs, geo + production.Geo)
    override this.ToString() = $"Resource[{ore} {clay} {obs} {geo}]"
    
    static member empty = Resources(0,0,0,0)

type Robot(material: Material, cost: Resources, production: Production) =
    member this.Material = material
    member this.Cost = cost
    member this.Production = production
    override this.ToString() = $"Robot({material} cost:{cost}"

    static member init(material: Material, cost: Resources) =
        let ore = if material = Ore then 1 else 0
        let clay = if material = Clay then 1 else 0
        let obsidian = if material = Obsidian then 1 else 0
        let geode = if material = Geode then 1 else 0
        let production = Production(ore, clay, obsidian, geode)
        Robot(material, cost, production)

type BluePrint(id: int, ore: Robot, clay: Robot, obsidian: Robot, geode: Robot) =
    member this.Id = id
    member this.Ore = ore
    member this.Clay = clay
    member this.Obsidian = obsidian
    member this.Geode = geode

    override this.ToString() =
        $"BluePrint({id}): {ore} {clay} {obsidian} {geode}"
