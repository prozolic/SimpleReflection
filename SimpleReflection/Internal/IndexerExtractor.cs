using System;
using System.Reflection;

namespace SimpleReflection.Internal
{
    internal class IndexerExtractor
    {
        public static string Extract(Type target)
        {
            var attr = target.GetCustomAttribute<DefaultMemberAttribute>();
            ErrorHelper.ThrowNullReferenceException(attr, "No Indexer is defined for the target Type.");
            return attr!.MemberName;
        }
    }
}
