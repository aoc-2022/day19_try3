module day19_try3.Input

open System.IO
open day19_try3.Base

let private parse (s: string) =
    let s = s.Split [| ' '; ':' |]
    let bluePrint = int s[1]
    let oreOre = int s[7]
    let clayOre = int s[13]
    let obsOre = int s[19]
    let obsClay = int s[22]
    let geoOre = int s[28]
    let geoObs = int s[31]
    let oreRobot = Robot.init (Ore, Resources(oreOre, 0, 0, 0))
    let clayRobot = Robot.init (Clay, Resources(clayOre, 0, 0, 0))
    let obsidianRobot = Robot.init (Obsidian, Resources(obsOre, obsClay, 0, 0))
    let geodeRobot = Robot.init (Geode, Resources(geoOre, 0, geoObs, 0))
    BluePrint(bluePrint, oreRobot,clayRobot, obsidianRobot, geodeRobot)

let read (fileName: string) =
    File.ReadAllLines fileName |> Seq.map parse |> Seq.toList