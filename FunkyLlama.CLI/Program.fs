// For more information see https://aka.ms/fsharp-console-apps

open System
open LLama.Common
open LLama.Native

module Test =
    
    let run () =
        let modelPath = ""
        
        Console.ForegroundColor <- ConsoleColor.DarkGray
        let modelParams = ModelParams(modelPath)
        
        
        
        
        ()


printfn "Hello from F#"