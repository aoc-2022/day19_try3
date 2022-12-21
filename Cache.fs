module day19_try3.Cache

open day19_try3.Base
open day19_try3.Track
open day19_try3.PreCalced

type Cache(cache:Map<int*Option<Material>,Production*Resources>, preCalced:PreCalced) =
    let cut (production:Production) (resources:Resources) (pre: PreCalced) : Resources =
        let maxOre = pre.MaxNeeded Ore
        let maxClay = pre.MaxNeeded Clay
        let maxObs = pre.MaxNeeded Obsidian
        let ore = if production.Ore < maxOre || resources.Ore < maxOre then resources.Ore else maxOre 
        let clay = if production.Clay < maxOre || resources.Clay < maxOre then resources.Clay else maxClay 
        let obs = if production.Obs < maxOre || resources.Obs < maxOre then resources.Obs else maxObs
        Resources(ore,clay,obs,resources.Geo)
        
    member this.Register (track:Track) =
        let key = track.Time.Left,(track.Build |> Option.map (fun r -> r.Material))
        let res = cut track.Production track.Resources preCalced
        if cache.ContainsKey key |> not then
            Cache(cache.Add(key, (track.Production,res)),preCalced)
        else 
            let cProd,cRes = cache[key]
            if track.Production.Ore < cProd.Ore then this
            elif track.Production.Clay < cProd.Clay then this
            elif track.Production.Obs < cProd.Obs then this
            elif track.Production.Geo < cProd.Geo then this 
            elif res.Ore < cRes.Ore then this
            elif res.Clay < cRes.Clay then this
            elif res.Obs < cRes.Obs then this
            elif res.Geo < cRes.Geo then this
            else
                printfn $"CACHE: NEW({track.Time.Left} -> {track.Production},{res})"
                Cache(cache.Add(key,(track.Production,res)),preCalced)
            
    member this.isWorse (track:Track) : bool =
        let key = track.Time.Left,(track.Build |> Option.map (fun r -> r.Material))
        let res = cut track.Production track.Resources preCalced
        if cache.ContainsKey key then
            let cProd,cRes = cache[key]
            if track.Production.Ore > cProd.Ore then false 
            elif track.Production.Clay > cProd.Clay then false 
            elif track.Production.Obs > cProd.Obs then false 
            elif track.Production.Geo > cProd.Geo then false
            elif res.Ore > cRes.Ore then false 
            elif res.Clay > cRes.Clay then false 
            elif res.Obs > cRes.Obs then false 
            elif res.Geo > cRes.Geo then false
            else true
        else false 
        
    override this.ToString() = $"Cache()"
    static member init (preCalced: PreCalced) = Cache(Map.empty, preCalced)
    