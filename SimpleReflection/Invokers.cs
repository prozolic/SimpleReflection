using System;

namespace SimpleReflection
{
    public sealed class ActionInvoker
    {
        private readonly Action<object, object[]?> _action;

        internal ActionInvoker(object action)
        {
            _action = (Action<object, object[]?>)action;
        }

        public void Invoke<T>(T target, params object[]? parameters)
        {
            _action.Invoke(target!, parameters);
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
            return _function.Invoke(target!, parameters);
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

    public sealed class GetPropertyInvoker
    {
        private readonly FunctionInvoker _getter;

        internal GetPropertyInvoker(FunctionInvoker getter)
        {
            _getter = getter;
        }

        public object? Invoke<T>(T target)
        {
            return _getter.Invoke(target, null);
        }
    }

    public sealed class SetPropertyInvoker
    {
        private readonly ActionInvoker _setter;

        internal SetPropertyInvoker(ActionInvoker getter)
        {
            _setter = getter;
        }

        public void Invoke<T>(T target, object? value)
        {
            _setter.Invoke(target, value!);
        }
    }

    public sealed class StaticGetPropertyInvoker
    {
        private readonly StaticFunctionInvoker _getter;

        internal StaticGetPropertyInvoker(StaticFunctionInvoker getter)
        {
            _getter = getter;
        }

        public object? Invoke()
        {
            return _getter.Invoke(null);
        }
    }

    public sealed class StaticSetPropertyInvoker
    {
        private readonly StaticActionInvoker _setter;

        internal StaticSetPropertyInvoker(StaticActionInvoker getter)
        {
            _setter = getter;
        }

        public void Invoke(object? value)
        {
            _setter.Invoke(value!);
        }
    }

    public sealed class IndexedGetPropertyInvoker
    {
        private readonly FunctionInvoker _getter;

        internal IndexedGetPropertyInvoker(FunctionInvoker getter)
        {
            _getter = getter;
        }

        public object? Invoke<T>(T target, params object[]? indexerParameters)
        {
            return _getter.Invoke(target, indexerParameters);
        }
    }

    public sealed class IndexedSetPropertyInvoker
    {
        public readonly ActionInvoker _setter;

        internal IndexedSetPropertyInvoker(ActionInvoker setter)
        {
            _setter = setter;
        }

        public void Invoke<T>(T target, object? value, params object[]? indexerParameters)
        {
            _setter.Invoke(target, this.ConcatArgs(value, indexerParameters));
        }

        private object[] ConcatArgs(object? value, params object[]? indexerParameters)
        {
            var parameterCount = indexerParameters?.Length ?? 0;
            var args = new object[parameterCount + 1];

            for (int i = 0; i < parameterCount; i++)
            {
                args[i] = indexerParameters![i];
            }
            args[indexerParameters?.Length ?? 0] = value!;
            return args;
        }
    }

}
