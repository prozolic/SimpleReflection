using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Diagnostics.Tracing.Parsers.Clr;
using SimpleReflection;

namespace SimpleReflection.BenchmarkTest
{
    [MemoryDiagnoser]
    [RankColumn]
    public class BenchmarkTest
    {
        [Params(100,10000)]
        public int Count { get; set; }

        private TestClass _testClass;

        private static MethodInfo? _testClassExecute;
        private static ActionInvoker _testClassExecuteInvoker;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _testClass = new();
            _testClassExecute = typeof(TestClass)!.GetMethod(
                "Execute",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            _testClassExecuteInvoker = typeof(TestClass).GenerateAction("Execute");
        }


        [Benchmark]
        public void MethodInfoCreate()
        {
            var method = typeof(TestClass).GetMethod(
            "Execute",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        }

        [Benchmark]
        public void InvokerCreate()
        {
            var invoker = typeof(TestClass).GenerateAction("Execute");
        }

        [Benchmark]
        public void MethodInfoNoCache()
        {
            for (int i = 0; i < this.Count; i++)
            {
                var method = typeof(TestClass).GetMethod(
                "Execute",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                method.Invoke(_testClass, null);
            }
        }

        [Benchmark]
        public void MethodInfoCache()
        {
            for (int i = 0; i < this.Count; i++)
            {
                _testClassExecute.Invoke(_testClass, null);
            }
        }

        [Benchmark]
        public void InvokerNoCache()
        {
            for (int i = 0; i < this.Count; i++)
            {
                var invoker = typeof(TestClass).GenerateAction("Execute");
                _testClass.InvokeAction(invoker);
            }
        }

        [Benchmark]
        public void InvokerCache()
        {
            for (int i = 0; i < this.Count; i++)
            {
                _testClass.InvokeAction(_testClassExecuteInvoker);
            }
        }

    }
}
