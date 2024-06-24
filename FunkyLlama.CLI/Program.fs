// For more information see https://aka.ms/fsharp-console-apps

open System
open System.Collections.Generic
open FSharp.Control
open FSharp.Control.TaskSeqExtensions
open LLama
open LLama.Common
open LLama.Native
open Microsoft.FSharp.Control

module Test =
    
    let run () =
        let modelPath = "/home/max/Data/LLMs/llama_2/Llama-2-7b-chat-hf-finetune-q5_k_m-v1.0.gguf"
        
        Console.ForegroundColor <- ConsoleColor.DarkGray
        let modelParams = ModelParams(modelPath)
        
        use weights = LLamaWeights.LoadFromFile(modelParams)
        
        use ctx = weights.CreateContext(modelParams)
        
        let ex = InteractiveExecutor(ctx)
        
        let session = ChatSession(ex)
        
        let hideWords = LLamaTransforms.KeywordTextOutputStreamTransform([ "User:"; "Bot: " ])
        session.WithOutputTransform(hideWords) |> ignore
        
        let infParam = InferenceParams()
        
        infParam.Temperature <- 0.6f
        infParam.AntiPrompts <- [ "User:" ]
        
        while true do
            
            Console.ForegroundColor <- ConsoleColor.Green
            Console.Write("\nQuestion: ")
            let userInput = Console.ReadLine()
            let msg =ChatHistory.Message(AuthorRole.User, "Question: " + userInput) 
            
            Console.ForegroundColor <- ConsoleColor.Yellow
        
            session.ChatAsync(msg, infParam)
            |> TaskSeq.iter Console.Write
            |> Async.AwaitTask
            |> Async.RunSynchronously
            
        
        ()


Test.run ()