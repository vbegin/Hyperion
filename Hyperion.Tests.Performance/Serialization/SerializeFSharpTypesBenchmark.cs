﻿#region copyright
// -----------------------------------------------------------------------
//  <copyright file="SerializeFSharpTypesBenchmark.cs" company="Akka.NET Team">
//      Copyright (C) 2015-2016 AsynkronIT <https://github.com/AsynkronIT>
//      Copyright (C) 2016-2016 Akka.NET Team <https://github.com/akkadotnet>
//  </copyright>
// -----------------------------------------------------------------------
#endregion

using Hyperion.FSharpTestTypes;
using Microsoft.FSharp.Collections;
using Microsoft.FSharp.Core;
using NBench;
using Pro.NBench.xUnit.XunitExtensions;
using Xunit.Abstractions;

namespace Hyperion.Tests.Performance.Serialization
{
    public class SerializeFSharpTypesBenchmark : PerfTestBase
    {
#if !NBENCH
        public SerializeFSharpTypesBenchmark(ITestOutputHelper output) : base(output)
        {
        }
#endif

        [NBenchFact]
        [PerfBenchmark(
            Description = "Benchmark discriminated union serialization",
            NumberOfIterations = StandardIterationCount,
            RunMode = RunMode.Throughput,
            RunTimeMilliseconds = StandardRunTime,
            TestMode = TestMode.Test)]
        [CounterThroughputAssertion(TestCounterName, MustBe.GreaterThan, 900000)]
        public void Serialize_DiscriminatedUnion()
        {
            SerializeAndCount(DU2.NewC(DU1.NewB("test", 123)));
        }

        [NBenchFact]
        [PerfBenchmark(
            Description = "Benchmark F# record serialization",
            NumberOfIterations = StandardIterationCount,
            RunMode = RunMode.Throughput,
            RunTimeMilliseconds = StandardRunTime,
            TestMode = TestMode.Test)]
        [CounterThroughputAssertion(TestCounterName, MustBe.GreaterThan, 600000)]
        public void Serialize_Record()
        {
            var record = new User(
                name: "John Doe",
                aref: FSharpOption<string>.Some("ok"),
                connections: "test");
            SerializeAndCount(record);
        }

        [NBenchFact]
        [PerfBenchmark(
            Description = "Benchmark F# record with map serialization",
            NumberOfIterations = StandardIterationCount,
            RunMode = RunMode.Throughput,
            RunTimeMilliseconds = StandardRunTime,
            TestMode = TestMode.Test)]
        [CounterThroughputAssertion(TestCounterName, MustBe.GreaterThan, 250)] //FIXME: F# Maps are pretty expensive
        public void Serialize_RecordWithMap()
        {
            var record = TestMap.createRecordWithMap;
            SerializeAndCount(record);
        }

        [NBenchFact]
        [PerfBenchmark(
            Description = "Benchmark F# list serialization",
            NumberOfIterations = StandardIterationCount,
            RunMode = RunMode.Throughput,
            RunTimeMilliseconds = StandardRunTime,
            TestMode = TestMode.Test)]
        [CounterThroughputAssertion(TestCounterName, MustBe.GreaterThan, 700000)]
        public void Serialize_FSharpList()
        {
            var list = ListModule.OfArray(new[] { 123, 2342355, 456456467578, 234234, -234281 });
            SerializeAndCount(list);
        }

        [NBenchFact]
        [PerfBenchmark(
            Description = "Benchmark F# set serialization",
            NumberOfIterations = StandardIterationCount,
            RunMode = RunMode.Throughput,
            RunTimeMilliseconds = StandardRunTime,
            TestMode = TestMode.Test)]
        [CounterThroughputAssertion(TestCounterName, MustBe.GreaterThan, 300000)]
        public void Serialize_FSharpSet()
        {
            var set = SetModule.OfArray(new[] { 123, 2342355, 456456467578, 234234, -234281 });
            SerializeAndCount(set);
        }
    }
}