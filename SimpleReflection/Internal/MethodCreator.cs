using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SimpleReflection.Internal
{
#nullable enable

    internal sealed class MethodCreator
    {
        private static readonly ParameterExpression _instanceParameter = Expression.Parameter(typeof(object), "instance");
        private static readonly ParameterExpression _argsParameter = Expression.Parameter(typeof(object[]), "args");

        public static (object method, DelegateType type) CreateStatic(Type target, string methodName)
        {
            var methodInfo = target.GetMethod(
                methodName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            return BuildStaticMethod(methodInfo);
        }

        public static (object method, DelegateType type) CreateStatic(Type target, string methodName, ArgsTypeArray argsTypeArray)
        {
            var methodInfo = target.GetMethod(
                methodName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static,
                null, argsTypeArray.Array, null);

            return BuildStaticMethod(methodInfo);
        }

        public static (object method, DelegateType type) CreateInstance(Type target, string methodName)
        {
            var methodInfo = target.GetMethod(
                methodName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            ErrorHelper.ThrowNullReferenceException(methodInfo, "Failed to get method information.");

            return BuildMethod(target, methodInfo);
        }

        public static (object method, DelegateType type) CreateInstance(Type target, string methodName, ArgsTypeArray argsTypeArray)
        {
            var methodInfo = target.GetMethod(
                methodName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                null, argsTypeArray.Array, null);

            ErrorHelper.ThrowNullReferenceException(methodInfo, "Failed to get method information.");

            return BuildMethod(target, methodInfo);
        }

        public static (object method, DelegateType type) BuildStaticMethod(MethodInfo? methodInfo)
        {
            ErrorHelper.ThrowNullReferenceException(methodInfo, "Failed to get method information.");

            var instance = _instanceParameter;
            var args = _argsParameter;
            var ps = methodInfo!.GetParameters().Select((x, index) =>
                Expression.Convert(
                    Expression.ArrayIndex(args, Expression.Constant(index)),
                x.ParameterType)).ToArray();

            if (methodInfo.ReturnType == typeof(void))
            {
                var lambda = Expression.Lambda<Action<object[]?>>(
                        Expression.Call(
                            methodInfo,
                            ps),
                    args).Compile();
                return (lambda, DelegateType.Action);
            }
            else
            {
                var lambda = Expression.Lambda<Func<object[]?, object>>(
                    Expression.Convert(
                        Expression.Call(
                            methodInfo
                            , ps),
                        typeof(object)),
                    args).Compile();
                return (lambda, DelegateType.Function);
            }
        }

        public static (object method, DelegateType type) BuildMethod(Type target, MethodInfo? methodInfo)
        {
            var instance = _instanceParameter;
            var args = _argsParameter;
            var ps = methodInfo!.GetParameters().Select((x, index) =>
                Expression.Convert(
                    Expression.ArrayIndex(args, Expression.Constant(index)),
                x.ParameterType)).ToArray();

            if (methodInfo.ReturnType == typeof(void))
            {
                var lambda = Expression.Lambda<Action<object, object[]?>>(
                    Expression.Call(
                        Expression.Convert(instance, target)
                        , methodInfo
                        , ps),
                    instance, args).Compile();
                return (lambda, DelegateType.Action);
            }
            else
            {
                var lambda = Expression.Lambda<Func<object, object[]?, object>>(
                    Expression.Convert(
                        Expression.Call(
                            Expression.Convert(instance, target)
                            , methodInfo
                            , ps),
                        typeof(object)),
                    instance, args).Compile();
                return (lambda, DelegateType.Function);
            }
        }
    }

#nullable disable
}
