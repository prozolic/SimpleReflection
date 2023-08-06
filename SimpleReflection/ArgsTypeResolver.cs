
namespace SimpleReflection.Internal
{
    public sealed class ArgsTypeResolver
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
            var argsTypes = new Type[args?.Length ?? 0];
            for (int i = 0; i < args?.Length; i++)
            {
                ErrorHelper.ThrowNullReferenceException(args[i], "The argument contains null");
                argsTypes[i]= args[i];
            }
            return new ArgsTypeArray(args);
        }

        public static Type ResolveType<T>(T target)
        {
            if (typeof(T) == typeof(object))
                return target.GetType();
            return typeof(T);
        }
    }
}
