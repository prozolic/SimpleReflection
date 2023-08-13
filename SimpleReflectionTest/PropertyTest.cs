using Xunit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using SimpleReflection;

namespace SimpleReflectionTest
{
    public class PropertyTest
    {
        public class Default
        {
            [Fact]
            public void PublicSetProperty()
            {
                var value = -1;
                {
                    var test = new TestProperty();
                    var invoker = test.GenerateSetProperty("Prop");
                    test.InvokeSetProperty(invoker, value);
                    Assert.Equal(value, test.Prop);
                }
                {
                    var test = new TestProperty();
                    var invoker = typeof(TestProperty).GenerateSetProperty("Prop");
                    test.InvokeSetProperty(invoker, value);
                    Assert.Equal(value, test.Prop);
                }
            }

            [Fact]
            public void PrivateSetProperty()
            {
                var value = -1;
                {
                    var test = new TestPrivateProperty();
                    var invoker = test.GenerateSetProperty("Prop");
                    test.InvokeSetProperty(invoker, value);
                    Assert.Equal(value, test.PropCheck);
                }
                {
                    var test = new TestPrivateProperty();
                    var invoker = typeof(TestPrivateProperty).GenerateSetProperty("Prop");
                    test.InvokeSetProperty(invoker, value);
                    Assert.Equal(value, test.PropCheck);
                }
            }

            [Fact]
            public void PublicStaticSetProperty()
            {
                var value = -1;
                {
                    var invoker = typeof(TestProperty).GenerateStaticSetProperty("PropStatic");
                    invoker.Invoke(value);
                    Assert.Equal(value, TestProperty.PropStatic);
                }
            }

            [Fact]
            public void PrivateStaticSetProperty()
            {
                var value = -1;
                {
                    var invoker = typeof(TestPrivateProperty).GenerateStaticSetProperty("PropStatic");
                    invoker.Invoke(value);
                    Assert.Equal(value, TestPrivateProperty.PropStaticCheck);
                }
            }

            [Fact]
            public void PublicGetProperty()
            {
                {
                    var test = new TestProperty();
                    var invoker = test.GenerateGetProperty("Prop");
                    var result = test.InvokeGetProperty(invoker);
                    Assert.IsType<int>(result);
                    Assert.Equal(test.Prop, result);
                }
                {
                    var test = new TestProperty();
                    var invoker = typeof(TestProperty).GenerateGetProperty("Prop");
                    var result = test.InvokeGetProperty(invoker);
                    Assert.IsType<int>(result);
                    Assert.Equal(test.Prop, result);
                }
            }

            [Fact]
            public void PublicGetReadOnlyProperty()
            {
                {
                    var test = new TestProperty();
                    var invoker = test.GenerateGetProperty("PropReadOnly");
                    var result = test.InvokeGetProperty(invoker);
                    Assert.IsType<int>(result);
                    Assert.Equal(test.PropReadOnly, result);
                }
                {
                    var test = new TestProperty();
                    var invoker = typeof(TestProperty).GenerateGetProperty("PropReadOnly");
                    var result = test.InvokeGetProperty(invoker);
                    Assert.IsType<int>(result);
                    Assert.Equal(test.PropReadOnly, result);
                }
            }

            [Fact]
            public void PrivateGetProperty()
            {
                {
                    var test = new TestPrivateProperty();
                    var invoker = test.GenerateGetProperty("Prop");
                    var result = test.InvokeGetProperty(invoker);
                    Assert.IsType<int>(result);
                    Assert.Equal(test.PropCheck, result);
                }
                {
                    var test = new TestPrivateProperty();
                    var invoker = typeof(TestPrivateProperty).GenerateGetProperty("Prop");
                    var result = test.InvokeGetProperty(invoker);
                    Assert.IsType<int>(result);
                    Assert.Equal(test.PropCheck, result);
                }
            }

            [Fact]
            public void PrivateGetReadOnlyProperty()
            {
                {
                    var test = new TestPrivateProperty();
                    var invoker = test.GenerateGetProperty("PropReadOnly");
                    var result = test.InvokeGetProperty(invoker);
                    Assert.IsType<int>(result);
                    Assert.Equal(test.PropReadOnlyCheck, result);
                }
                {
                    var test = new TestPrivateProperty();
                    var invoker = typeof(TestPrivateProperty).GenerateGetProperty("PropReadOnly");
                    var result = test.InvokeGetProperty(invoker);
                    Assert.IsType<int>(result);
                    Assert.Equal(test.PropReadOnlyCheck, result);
                }
            }

            [Fact]
            public void PublicStaticGetProperty()
            {
                {
                    var invoker = typeof(TestProperty).GenerateStaticGetProperty("PropStatic");
                    var result = invoker.Invoke();
                    Assert.IsType<int>(result);
                    Assert.Equal(TestProperty.PropStatic, result);
                }
            }

            [Fact]
            public void PublicStaticGetReadOnlyProperty()
            {
                {
                    var invoker = typeof(TestProperty).GenerateStaticGetProperty("PropReadOnlyStatic");
                    var result = invoker.Invoke();
                    Assert.IsType<int>(result);
                    Assert.Equal(TestProperty.PropReadOnlyStatic, result);
                }
            }

            [Fact]
            public void PrivateStaticGetProperty()
            {
                {
                    var invoker = typeof(TestPrivateProperty).GenerateStaticGetProperty("PropStatic");
                    var result = invoker.Invoke();
                    Assert.IsType<int>(result);
                    Assert.Equal(TestPrivateProperty.PropStaticCheck, result);
                }
            }

            [Fact]
            public void PrivateStaticGetReadOnlyProperty()
            {
                {
                    var invoker = typeof(TestPrivateProperty).GenerateStaticGetProperty("PropReadOnlyStatic");
                    var result = invoker.Invoke();
                    Assert.IsType<int>(result);
                    Assert.Equal(TestPrivateProperty.PropReadOnlyStaticCheck, result);
                }
            }
        }

        public class Error
        {
            [Fact]
            public void NullName()
            {
                Assert.Throws<ArgumentNullException>(() => new TestFunction().GenerateSetProperty(null!));
                Assert.Throws<ArgumentNullException>(() => typeof(TestFunction).GenerateSetProperty(null!));
                Assert.Throws<ArgumentNullException>(() => new TestFunction().GenerateSetProperty(""));
                Assert.Throws<ArgumentNullException>(() => typeof(TestFunction).GenerateSetProperty(""));

                Assert.Throws<ArgumentNullException>(() => typeof(TestAction).GenerateStaticSetProperty(null!));
                Assert.Throws<ArgumentNullException>(() => typeof(TestAction).GenerateStaticSetProperty(""));

                Assert.Throws<ArgumentNullException>(() => new TestFunction().GenerateGetProperty(null!));
                Assert.Throws<ArgumentNullException>(() => typeof(TestFunction).GenerateGetProperty(null!));
                Assert.Throws<ArgumentNullException>(() => new TestFunction().GenerateGetProperty(""));
                Assert.Throws<ArgumentNullException>(() => typeof(TestFunction).GenerateGetProperty(""));

                Assert.Throws<ArgumentNullException>(() => typeof(TestAction).GenerateStaticGetProperty(null!));
                Assert.Throws<ArgumentNullException>(() => typeof(TestAction).GenerateStaticGetProperty(""));
            }

            [Fact]
            public void MissName()
            {
                Assert.Throws<NullReferenceException>(() => new TestFunction().GenerateSetProperty("Error"));
                Assert.Throws<NullReferenceException>(() => typeof(TestFunction).GenerateSetProperty("Error"));
                Assert.Throws<NullReferenceException>(() => typeof(TestAction).GenerateStaticSetProperty("Error"));

                Assert.Throws<NullReferenceException>(() => new TestFunction().GenerateGetProperty("Error"));
                Assert.Throws<NullReferenceException>(() => typeof(TestFunction).GenerateGetProperty("Error"));
                Assert.Throws<NullReferenceException>(() => typeof(TestAction).GenerateStaticGetProperty("Error"));
            }
        }
    }
}
