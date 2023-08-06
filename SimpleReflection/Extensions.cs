using SimpleReflection.Internal;

namespace SimpleReflection
{
    public static class Extensions
    {
        public static object? ExecuteInvoker<T>(this T target, MethodInvoker invoker, params object[] args)
        {
            ErrorHelper.ThrowArgumentNullException(invoker, nameof(invoker));

            return invoker.Invoke(target, args);
        }
        public static object? ExecuteStaticInvoker(this MethodInvoker invoker, params object[] args)
        {
            return invoker.Invoke(args);
        }

        public static MethodInvoker GenerateMethod<T>(this T target, string methodName)
        {
            ErrorHelper.ThrowArgumentNullException(methodName, nameof(methodName));

            return MethodInvoker.CreateInstance(target, methodName);
        }

        public static MethodInvoker GenerateMethodArgs<T>(this T target, string methodName, ArgsTypeArray argsTypeArray)
        {
            ErrorHelper.ThrowArgumentNullException(methodName, nameof(methodName));

            return MethodInvoker.CreateInstance(target, methodName, argsTypeArray);
        }

        public static MethodInvoker GenerateStaticMethod(this Type target, string methodName)
        {
            return MethodInvoker.CreateStatic(target, methodName);
        }

        public static MethodInvoker GenerateStaticMethodArgs(this Type target, string methodName, ArgsTypeArray argsTypeArray)
        {
            return MethodInvoker.CreateStatic(target, methodName, argsTypeArray);
        }
    }
}
