using System;

namespace SimpleReflection.Internal
{
    public sealed class ArgsTypeArrayResolver
    {
        public static ArgsTypeArray ResolveRealParameter(params object[]? args)
        {
            var argsTypes = new Type[args?.Length ?? 0];
            for (int i = 0; i < args?.Length; i++)
            {
                ErrorHelper.ThrowNullReferenceException(args[i], "The argument contains null");
                argsTypes[i] = ResolveType(args[i]);
            }
            return new ArgsTypeArray(argsTypes);
        }

        public static ArgsTypeArray Resolve(params Type[]? args)
        {
            ErrorHelper.ThrowArgumentNullException(args, nameof(args));
            for (int i = 0; i < args?.Length; i++)
            {
                ErrorHelper.ThrowNullReferenceException(args[i], "The argument contains null");
            }
            return new ArgsTypeArray(args!);
        }

        private static Type ResolveType<T>(T target)
        {
            if (typeof(T) == typeof(object))
                return target!.GetType();
            return typeof(T);
        }
    }
}
