using SimpleReflection.Internal;

namespace SimpleReflection
{
    public static class Extensions
    {
        public static ActionInvoker GenerateAction(this Type target, string methodName)
        {
            ErrorHelper.ThrowArgumentNullException(methodName, nameof(methodName));

            var m = MethodCreator.CreateInstance(target, methodName);
            return new ActionInvoker(m.method);
        }

        public static ActionInvoker GenerateAction<T>(this T target, string methodName)
        {
            return target.GetType().GenerateAction(methodName);
        }

        public static ActionInvoker GenerateActionArgs(this Type target, string methodName, ArgsTypeArray argsTypeArray)
        {
            ErrorHelper.ThrowArgumentNullException(methodName, nameof(methodName));

            var m = MethodCreator.CreateInstance(target, methodName, argsTypeArray);
            return new ActionInvoker(m.method);
        }

        public static ActionInvoker GenerateActionArgs<T>(this T target, string methodName, ArgsTypeArray argsTypeArray)
        {
            ErrorHelper.ThrowArgumentNullException(methodName, nameof(methodName));

            var m = MethodCreator.CreateInstance(target.GetType(), methodName, argsTypeArray);
            return new ActionInvoker(m.method);
        }

        public static FunctionInvoker GenerateFunction(this Type target, string methodName)
        {
            ErrorHelper.ThrowArgumentNullException(methodName, nameof(methodName));

            var m = MethodCreator.CreateInstance(target, methodName);
            return new FunctionInvoker(m.method);
        }

        public static FunctionInvoker GenerateFunction<T>(this T target, string methodName)
        {
            return target.GetType().GenerateFunction(methodName);
        }

        public static FunctionInvoker GenerateFunctionArgs(this Type target, string methodName, ArgsTypeArray argsTypeArray)
        {
            ErrorHelper.ThrowArgumentNullException(methodName, nameof(methodName));

            var m = MethodCreator.CreateInstance(target, methodName, argsTypeArray);
            return new FunctionInvoker(m.method);
        }

        public static FunctionInvoker GenerateFunctionArgs<T>(this T target, string methodName, ArgsTypeArray argsTypeArray)
        {
            ErrorHelper.ThrowArgumentNullException(methodName, nameof(methodName));

            var m = MethodCreator.CreateInstance(target.GetType(), methodName, argsTypeArray);
            return new FunctionInvoker(m.method);
        }

        public static StaticActionInvoker GenerateStaticAction(this Type target, string methodName)
        {
            ErrorHelper.ThrowArgumentNullException(methodName, nameof(methodName));

            var m = MethodCreator.CreateStatic(target, methodName);
            return new StaticActionInvoker(m.method);
        }

        public static StaticActionInvoker GenerateStaticActionArgs(this Type target, string methodName, ArgsTypeArray argsTypeArray)
        {
            ErrorHelper.ThrowArgumentNullException(methodName, nameof(methodName));

            var m = MethodCreator.CreateStatic(target, methodName, argsTypeArray);
            return new StaticActionInvoker(m.method);
        }

        public static StaticFunctionInvoker GenerateStaticFunction(this Type target, string methodName)
        {
            ErrorHelper.ThrowArgumentNullException(methodName, nameof(methodName));

            var m = MethodCreator.CreateStatic(target, methodName);
            return new StaticFunctionInvoker(m.method);
        }

        public static StaticFunctionInvoker GenerateStaticFunctionArgs(this Type target, string methodName, ArgsTypeArray argsTypeArray)
        {
            ErrorHelper.ThrowArgumentNullException(methodName, nameof(methodName));

            var m = MethodCreator.CreateStatic(target, methodName, argsTypeArray);
            return new StaticFunctionInvoker(m.method);
        }

        public static void InvokeAction<T>(this T target, ActionInvoker invoker, params object[] args)
        {
            ErrorHelper.ThrowArgumentNullException(invoker, nameof(invoker));

            invoker.Invoke(target, args);
        }

        public static object? InvokeFunction<T>(this T target, FunctionInvoker invoker, params object[] args)
        {
            ErrorHelper.ThrowArgumentNullException(invoker, nameof(invoker));

            return invoker.Invoke(target, args);
        }

        public sealed class ActionInvoker
        {
            private readonly Action<object, object[]?> _action;

            internal ActionInvoker(object action)
            {
                _action = (Action<object, object[]?>)action;
            }

            public void Invoke<T>(T target, params object[]? parameters)
            {
                _action.Invoke(target, parameters);
            }
        }

        public sealed class StaticActionInvoker
        {
            private readonly Action<object[]?> _action;

            internal StaticActionInvoker(object action)
            {
                _action = (Action<object[]?>)action;
            }

            public void Invoke(params object[]? parameters)
            {
                _action.Invoke(parameters);
            }
        }

        public sealed class FunctionInvoker
        {
            private readonly Func<object, object[]?, object> _function;

            internal FunctionInvoker(object function)
            {
                _function = (Func<object, object[]?, object>)function;
            }

            public object? Invoke<T>(T target, params object[]? parameters)
            {
                return _function.Invoke(target, parameters);
            }
        }

        public sealed class StaticFunctionInvoker
        {
            private readonly Func<object[]?, object> _function;

            internal StaticFunctionInvoker(object function)
            {
                _function = (Func<object[]?, object>)function;
            }

            public object? Invoke(params object[]? parameters)
            {
                return _function.Invoke(parameters);
            }
        }

    }
}
