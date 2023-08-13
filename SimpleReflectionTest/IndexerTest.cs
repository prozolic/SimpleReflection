using SimpleReflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleReflectionTest
{
    public class IndexerTest
    {
        public class Default
        {
            [Fact]
            public void PublicSetIndex()
            {
                var index = 1;
                var indexValue = 999;
                {
                    var test = new TestIndexer();
                    var invoker = test.GenerateIndexedSetProperty();
                    test.InvokeIndexedSetProperty(invoker, indexValue, index);
                    Assert.Equal(indexValue, test[index]);
                }
                {
                    var test = new TestIndexer();
                    var invoker = typeof(TestIndexer).GenerateIndexedSetProperty();
                    test.InvokeIndexedSetProperty(invoker, indexValue, index);
                    Assert.Equal(indexValue, test[index]);
                }
            }

            [Fact]
            public void PrivateSetIndex()
            {
                var index = 1;
                var indexValue = 999;
                {
                    var test = new TestPrivateIndexer();
                    var invoker = test.GenerateIndexedSetProperty();
                    test.InvokeIndexedSetProperty(invoker, indexValue, index);
                    Assert.Equal(indexValue, test.ItemCheck[index]);
                }
                {
                    var test = new TestPrivateIndexer();
                    var invoker = typeof(TestPrivateIndexer).GenerateIndexedSetProperty();
                    test.InvokeIndexedSetProperty(invoker, indexValue, index);
                    Assert.Equal(indexValue, test.ItemCheck[index]);
                }
            }

            [Fact]
            public void PublicGetIndex()
            {
                var index = 1;
                {
                    var test = new TestIndexer();
                    var invoker = test.GenerateIndexedGetProperty();
                    var result = test.InvokeIndexedGetProperty(invoker, index);
                    Assert.IsType<int>(result);
                    Assert.Equal(result, test[index]);
                }
                {
                    var test = new TestIndexer();
                    var invoker = typeof(TestIndexer).GenerateIndexedGetProperty();
                    var result = test.InvokeIndexedGetProperty(invoker, index);
                    Assert.IsType<int>(result);
                    Assert.Equal(result, test[index]);
                }
            }

            [Fact]
            public void PrivateGetIndex()
            {
                var index = 1;
                {
                    var test = new TestPrivateIndexer();
                    var invoker = test.GenerateIndexedGetProperty();
                    var result = test.InvokeIndexedGetProperty(invoker, index);
                    Assert.IsType<int>(result);
                    Assert.Equal(result, test.ItemCheck[index]);
                }
                {
                    var test = new TestPrivateIndexer();
                    var invoker = typeof(TestPrivateIndexer).GenerateIndexedGetProperty();
                    var result = test.InvokeIndexedGetProperty(invoker, index);
                    Assert.IsType<int>(result);
                    Assert.Equal(result, test.ItemCheck[index]);
                }
            }
        }

        public class Error
        {

        }
    }
}
