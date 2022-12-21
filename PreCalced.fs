module day19_try3.PreCalced

open day19_try3.Base

type PreCalced(bluePrint: BluePrint) =
    
    member this.RobotOptions : Option<Robot> list =
        let all = [bluePrint.Geode;bluePrint.Obsidian;bluePrint.Clay;bluePrint.Ore]
        [all |> List.map Some;[None]] |> List.concat
    member this.Foo = "foo"

