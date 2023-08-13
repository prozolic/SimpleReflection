using Xunit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using SimpleReflection;

namespace SimpleReflectionTest
{
    public class ActionTest
    {
        [Fact]
        public void PublicAction()
        {
            var test = new TestAction();
            {
                var invoker = test.GenerateAction("Execute");
                test.InvokeAction(invoker);
            }
            {
                var invoker = typeof(TestAction).GenerateAction("Execute");
                test.InvokeAction(invoker);
            }
        }
        
        [Fact]
        public void PrivateAction()
        {
            var test = new TestPrivateAction();
            {
                var invoker = test.GenerateAction("Execute");
                test.InvokeAction(invoker);
            }
            {
                var invoker = typeof(TestPrivateAction).GenerateAction("Execute");
                test.InvokeAction(invoker);
            }
        }

        [Fact]
        public void PublicActionArgs()
        {
            var test = new TestAction();
            {
                var invoker = test.GenerateActionArgs("Execute2", ArgsTypeArray.Empty);
                test.InvokeAction(invoker);
            }
            {
                var invoker = typeof(TestAction).GenerateActionArgs("Execute2", ArgsTypeArray.Empty);
                test.InvokeAction(invoker);
            }
            {
                var invoker = test.GenerateActionArgs("Execute2", ArgsTypeArrayResolver.Resolve(typeof(string))); ;
                test.InvokeAction(invoker, "test");
            }
            {
                var invoker = typeof(TestAction).GenerateActionArgs("Execute2", ArgsTypeArrayResolver.Resolve(typeof(string))); ;
                test.InvokeAction(invoker, "test");
            }
        }

        [Fact]
        public void PrivateActionArgs()
        {
            var test = new TestPrivateAction();
            {
                var invoker = test.GenerateActionArgs("Execute2", ArgsTypeArray.Empty);
                test.InvokeAction(invoker);
            }
            {
                var invoker = typeof(TestPrivateAction).GenerateActionArgs("Execute2", ArgsTypeArray.Empty);
                test.InvokeAction(invoker);
            }
            {
                var invoker = test.GenerateActionArgs("Execute2", ArgsTypeArrayResolver.Resolve(typeof(string))); ;
                test.InvokeAction(invoker, "test");
            }
            {
                var invoker = typeof(TestPrivateAction).GenerateActionArgs("Execute2", ArgsTypeArrayResolver.Resolve(typeof(string))); ;
                test.InvokeAction(invoker, "test");
            }
        }

        [Fact]
        public void PublicStaticAction()
        {
            {
                var invoker = typeof(TestAction).GenerateStaticAction("ExecuteStatic");
                invoker.Invoke();
            }
        }

        [Fact]
        public void PrivateStaticAction()
        {
            {
                var invoker = typeof(TestPrivateAction).GenerateStaticAction("ExecuteStatic");
                invoker.Invoke();
            }
        }

        [Fact]
        public void PublicStaticActionArgs()
        {
            {
                var invoker = typeof(TestAction).GenerateStaticActionArgs("ExecuteStatic2", ArgsTypeArray.Empty);
                invoker.Invoke();
            }
            {
                var invoker = typeof(TestAction).GenerateStaticActionArgs("ExecuteStatic2", ArgsTypeArrayResolver.Resolve(typeof(string))); ;
                invoker.Invoke("test");
            }
        }

        [Fact]
        public void PrivateStaticActionArgs()
        {
            {
                var invoker = typeof(TestPrivateAction).GenerateStaticActionArgs("ExecuteStatic2", ArgsTypeArray.Empty);
                invoker.Invoke();
            }
            {
                var invoker = typeof(TestPrivateAction).GenerateStaticActionArgs("ExecuteStatic2", ArgsTypeArrayResolver.Resolve(typeof(string))); ;
                invoker.Invoke("test");
            }
        }


    }
}