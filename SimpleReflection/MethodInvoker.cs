
using SimpleReflection.Internal;

namespace SimpleReflection
{
    public abstract class MethodInvoker
    {
        protected readonly object _method;

        protected MethodInvoker(object method)
        {
            _method = method;
            IsStatic = false;
        }
        internal bool IsStatic { get; private set; }
        internal abstract object? Invoke(params object[]? parameters);
        internal abstract object? Invoke<T>(T target, params object[]? parameters);

        public static MethodInvoker CreateStatic(Type classType, string methodName)
        {
            return CreateStatic(classType, methodName, ArgsTypeArray.Empty);
        }

        public static MethodInvoker CreateStatic(Type classType, string methodName, ArgsTypeArray argsTypeArray)
        {
            var m = MethodCreator.CreateStatic(classType, methodName, argsTypeArray);
            return new StaticMethodInvoker(m.method, m.type);
        }
        public static MethodInvoker CreateInstance<T>(T target, string methodName)
        {
            return CreateInstance(target, methodName, ArgsTypeArray.Empty);
        }
        public static MethodInvoker CreateInstance<T>(T target, string methodName, ArgsTypeArray argsTypeArray)
        {
            var m = MethodCreator.Create(target, methodName, argsTypeArray);
            return new InstanceMethodInvoker(m.method, m.type);
        }

        private sealed class StaticMethodInvoker : MethodInvoker
        {
            private readonly DelegateType _type;

            public StaticMethodInvoker(object method, DelegateType type) : base(method)
            {
                _type = type;
                IsStatic = true;
            }

            internal override object? Invoke(params object[]? parameters)
            {
                return InvokeStaticInternal(parameters);
            }

            internal override object? Invoke<T>(T target, params object[]? parameters)
            {
                throw new NotImplementedException("The method is not implemented.");
            }

            private object? InvokeStaticInternal(object[]? parameters)
            {
                if (_type == DelegateType.Action)
                {
                    (_method as Action<object[]?>).Invoke(parameters);
                    return null;
                }
                return (_method as Func<object[]?, object>).Invoke(parameters);
            }
        }

        private sealed class InstanceMethodInvoker : MethodInvoker
        {
            private readonly DelegateType _type;

            public InstanceMethodInvoker(object method, DelegateType type) : base(method)
            {
                _type = type;
            }

            internal override object? Invoke(params object[]? parameters)
            {
                throw new NotImplementedException("The method is not implemented.");
            }

            internal override object? Invoke<T>(T target, params object[]? parameters)
            {
                return this.InvokeInternal(target, parameters);
            }

            private object? InvokeInternal<T>(T target, object[]? parameters)
            {
                if (_type == DelegateType.Action)
                {
                    (_method as Action<object, object[]?>).Invoke(target, parameters);
                    return null;
                }
                return (_method as Func<object, object[]?, object>).Invoke(target, parameters);
            }
        }
    }
}