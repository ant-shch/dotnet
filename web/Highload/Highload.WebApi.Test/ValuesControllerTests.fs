module ValuesControllerTests

open System
open System.Threading.Tasks

open Xunit
open FSharp.Control.Tasks.V2.ContextInsensitive

open NBomber.Contracts
open NBomber.FSharp
open NBomber.Http.FSharp

let buildScenario () =

    let step = 
        HttpStep.create("send requests 1 seconds", fun context ->
            Http.createRequest "GET" "http://localhost:5050/api/values"
            |> Http.withHeader "Accept" "text/html"
            //|> Http.withBody(new StringContent("{ some JSON }", Encoding.UTF8, "application/json"))
            //|> Http.withVersion("1.1")            
            //|> Http.withCheck(fun response -> response.IsSuccessStatusCode |> Task.FromResult) // default check            
        )


    Scenario.create "Values Controller Test Scenario" [step]

[<Fact>]
let ``Send requests durring 1 second`` () =    
    
    let assertions = [       
       Assertion.forStep("send requests 1 seconds", (fun stats -> stats.OkCount > 2), "OkCount > 2")
       Assertion.forStep("send requests 1 seconds", (fun stats -> stats.RPS > 8), "RPS > 8")
       Assertion.forStep("send requests 1 seconds", (fun stats -> stats.Percent75 >= 2), "Percent75 >= 2")
    ]

    let scenario = 
        buildScenario()
        |> Scenario.withConcurrentCopies 1
        |> Scenario.withWarmUpDuration(TimeSpan.FromSeconds 0.0)
        |> Scenario.withDuration(TimeSpan.FromSeconds 1.0)
        |> Scenario.withAssertions assertions
    
    NBomberRunner.registerScenarios [scenario]
    |> NBomberRunner.runTest