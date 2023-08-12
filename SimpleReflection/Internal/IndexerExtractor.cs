using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace SimpleReflection.Internal
{
    internal class IndexerExtractor
    {
        public static string Extract(Type target)
        {
#pragma warning disable CS8602
            var attr = target.GetCustomAttribute<DefaultMemberAttribute>();
            ErrorHelper.ThrowNullReferenceException(attr, "No Indexer is defined for the target Type.");
            return attr.MemberName;

#pragma warning restore CS8602
        }
    }
}
