﻿namespace Fuchu

module FsCheckTests = 
    open Fuchu
    open Fuchu.Impl

    let properties = 
        testList "FsCheck" [
            testProperty "Addition is commutative" <|
                fun a b -> 
                    a + b = b + a
            testProperty "Product is distributive over addition" <|
                fun a b c -> 
                    a * (b + c) = a * a + a * c // wrong on purpose to test failures
        ]

    [<Tests>]
    let runFsCheckTests = 
        testCase "run" <| fun _ -> 
            let results = evalSilent properties
            Assert.Equal("results length", 2, results.Length)
            Assert.Equal("passed count", TestResult.Passed, results.[0].Result)
            match results.[1].Result with
            | TestResult.Failed _ -> ()
            | x -> failtestf "Expected Failed, actual %A" x
