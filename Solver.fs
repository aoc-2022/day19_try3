module day19_try3.Solver

open day19_try3.Base
open day19_try3.Track
open day19_try3.Cache
open day19_try3.PreCalced

let solve (initTime:Time) (bluePrint: BluePrint) =
    let preCalced = PreCalced(bluePrint)
    let rec solve (track:Track) (cache:Cache) =
        if CutOffs.shouldAbort track cache preCalced then
            // printfn $"ABORT: {track} {cache}"
            cache
        else
            printfn $"solve: {track} {cache}"
            let rec descend (robotOptions:Option<Robot> list) (cache: Cache) : Cache =
                match robotOptions with
                | [] -> cache
                | ro::left ->
                    let track = track.Tick ro
                    let cache = solve track cache 
                    descend left cache 
            descend preCalced.RobotOptions cache 
    solve (Track.initial initTime) Cache.empty
         
        
    