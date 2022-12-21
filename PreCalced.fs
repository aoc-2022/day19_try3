module day19_try3.PreCalced

open day19_try3.Base

type PreCalced(bluePrint: BluePrint) =
    member this.MaxNeeded (material:Material) =
        match material with
        | Ore -> [bluePrint.Clay.Cost.Ore;bluePrint.Obsidian.Cost.Ore;bluePrint.Geode.Cost.Ore] |> List.max
        | Clay -> bluePrint.Obsidian.Cost.Clay
        | Obsidian -> bluePrint.Geode.Cost.Obs
    
    member this.RobotOptions : Option<Robot> list =
        let all = [bluePrint.Geode;bluePrint.Obsidian;bluePrint.Clay;bluePrint.Ore]
        [all |> List.map Some;[None]] |> List.concat
    member this.Foo = "foo"

