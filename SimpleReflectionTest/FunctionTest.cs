using SimpleReflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleReflectionTest
{
    public class FunctionTest
    {
        [Fact]
        public void PublicFunction()
        {
            var test = new TestFunction();
            {
                var invoker = test.GenerateFunction("Execute");
                var result = test.InvokeFunction(invoker);
                Assert.IsType<int>(result);
                Assert.Equal(1, (int)result!);
            }
            {
                var invoker = typeof(TestFunction).GenerateFunction("Execute");
                var result = test.InvokeFunction(invoker);
                Assert.IsType<int>(result);
                Assert.Equal(1, (int)result!);
            }
        }

        [Fact]
        public void PrivateFunction()
        {
            var test = new TestPrivateFunction();
            {
                var invoker = test.GenerateFunction("Execute");
                var result = test.InvokeFunction(invoker);
                Assert.IsType<int>(result);
                Assert.Equal(1, (int)result!);
            }
            {
                var invoker = typeof(TestPrivateFunction).GenerateFunction("Execute");
                var result = test.InvokeFunction(invoker);
                Assert.IsType<int>(result);
                Assert.Equal(1, (int)result!);
            }
        }

        [Fact]
        public void PublicFunctionArgs()
        {
            var test = new TestFunction();
            {
                var invoker = test.GenerateFunctionArgs("Execute2", ArgsTypeArray.Empty);
                var result = test.InvokeFunction(invoker);
                Assert.IsType<int>(result);
                Assert.Equal(2, (int)result!);
            }
            {
                var invoker = typeof(TestFunction).GenerateFunctionArgs("Execute2", ArgsTypeArray.Empty);
                var result = test.InvokeFunction(invoker);
                Assert.IsType<int>(result);
                Assert.Equal(2, (int)result!);
            }
            {
                var invoker = test.GenerateFunctionArgs("Execute2", ArgsTypeArrayResolver.Resolve(typeof(string))); ;
                var result = test.InvokeFunction(invoker, "test");
                Assert.IsType<int>(result);
                Assert.Equal(2, (int)result!);
            }
            {
                var invoker = typeof(TestFunction).GenerateFunctionArgs("Execute2", ArgsTypeArrayResolver.Resolve(typeof(string))); ;
                var result = test.InvokeFunction(invoker, "test");
                Assert.IsType<int>(result);
                Assert.Equal(2, (int)result!);
            }
        }

        [Fact]
        public void PrivateFunctionArgs()
        {
            var test = new TestPrivateFunction();
            {
                var invoker = test.GenerateFunctionArgs("Execute2", ArgsTypeArray.Empty);
                var result = test.InvokeFunction(invoker);
                Assert.IsType<int>(result);
                Assert.Equal(2, (int)result!);
            }
            {
                var invoker = typeof(TestPrivateFunction).GenerateFunctionArgs("Execute2", ArgsTypeArray.Empty);
                var result = test.InvokeFunction(invoker);
                Assert.IsType<int>(result);
                Assert.Equal(2, (int)result!);
            }
            {
                var invoker = test.GenerateFunctionArgs("Execute2", ArgsTypeArrayResolver.Resolve(typeof(string))); ;
                var result = test.InvokeFunction(invoker, "test");
                Assert.IsType<int>(result);
                Assert.Equal(2, (int)result!);
            }
            {
                var invoker = typeof(TestPrivateFunction).GenerateFunctionArgs("Execute2", ArgsTypeArrayResolver.Resolve(typeof(string))); ;
                var result = test.InvokeFunction(invoker, "test");
                Assert.IsType<int>(result);
                Assert.Equal(2, (int)result!);
            }
        }

        [Fact]
        public void PublicStaticFunction()
        {
            {
                var invoker = typeof(TestFunction).GenerateStaticFunction("ExecuteStatic");
                var result = invoker.Invoke();
                Assert.IsType<int>(result);
                Assert.Equal(1, (int)result!);
            }
        }

        [Fact]
        public void PrivateStaticFunction()
        {
            {
                var invoker = typeof(TestPrivateFunction).GenerateStaticFunction("ExecuteStatic");
                var result = invoker.Invoke();
                Assert.IsType<int>(result);
                Assert.Equal(1, (int)result!);
            }
        }

        [Fact]
        public void PublicStaticFunctionArgs()
        {
            {
                var invoker = typeof(TestFunction).GenerateStaticFunctionArgs("ExecuteStatic2", ArgsTypeArray.Empty);
                var result = invoker.Invoke();
                Assert.IsType<int>(result);
                Assert.Equal(2, (int)result!);
            }
            {
                var invoker = typeof(TestFunction).GenerateStaticFunctionArgs("ExecuteStatic2", ArgsTypeArrayResolver.Resolve(typeof(string))); ;
                var result = invoker.Invoke("test");
                Assert.IsType<int>(result);
                Assert.Equal(2, (int)result!);
            }
        }

        [Fact]
        public void PrivateStaticFunctionArgs()
        {
            {
                var invoker = typeof(TestPrivateFunction).GenerateStaticFunctionArgs("ExecuteStatic2", ArgsTypeArray.Empty);
                var result = invoker.Invoke();
                Assert.IsType<int>(result);
                Assert.Equal(2, (int)result!);
            }
            {
                var invoker = typeof(TestPrivateFunction).GenerateStaticFunctionArgs("ExecuteStatic2", ArgsTypeArrayResolver.Resolve(typeof(string))); ;
                var result = invoker.Invoke("test");
                Assert.IsType<int>(result);
                Assert.Equal(2, (int)result!);
            }
        }

    }
}
