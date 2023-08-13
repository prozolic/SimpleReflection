using Xunit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using SimpleReflection;
using Xunit.Sdk;

namespace SimpleReflectionTest
{
    public class ActionTest
    {
        public class Default
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

        public class Error
        {
            [Fact]
            public void NullName()
            {
                Assert.Throws<ArgumentNullException>(() => new TestAction().GenerateAction(null!));
                Assert.Throws<ArgumentNullException>(() => typeof(TestAction).GenerateAction(null!));
                Assert.Throws<ArgumentNullException>(() => new TestAction().GenerateAction(""));
                Assert.Throws<ArgumentNullException>(() => typeof(TestAction).GenerateAction(""));

                Assert.Throws<ArgumentNullException>(() => new TestAction().GenerateActionArgs(null!, ArgsTypeArray.Empty));
                Assert.Throws<ArgumentNullException>(() => typeof(TestAction).GenerateActionArgs(null!, ArgsTypeArray.Empty));
                Assert.Throws<ArgumentNullException>(() => new TestAction().GenerateActionArgs("", ArgsTypeArray.Empty));
                Assert.Throws<ArgumentNullException>(() => typeof(TestAction).GenerateActionArgs("", ArgsTypeArray.Empty));

                Assert.Throws<ArgumentNullException>(() => typeof(TestAction).GenerateStaticAction(null!));
                Assert.Throws<ArgumentNullException>(() => typeof(TestAction).GenerateStaticAction(""));

                Assert.Throws<ArgumentNullException>(() => typeof(TestAction).GenerateStaticActionArgs(null!, ArgsTypeArray.Empty));
                Assert.Throws<ArgumentNullException>(() => typeof(TestAction).GenerateStaticActionArgs("", ArgsTypeArray.Empty));
            }

            [Fact]
            public void MissName()
            {
                Assert.Throws<NullReferenceException>(() => new TestAction().GenerateAction("Error"));
                Assert.Throws<NullReferenceException>(() => typeof(TestAction).GenerateAction("Error"));

                Assert.Throws<NullReferenceException>(() => new TestAction().GenerateActionArgs("Error", ArgsTypeArray.Empty));
                Assert.Throws<NullReferenceException>(() => typeof(TestAction).GenerateActionArgs("Error", ArgsTypeArray.Empty));

                Assert.Throws<NullReferenceException>(() => typeof(TestAction).GenerateStaticAction("Error"));
                Assert.Throws<NullReferenceException>(() => typeof(TestAction).GenerateStaticActionArgs("Error", ArgsTypeArray.Empty));
            }
        }

    }
}