﻿using System;
using SimpleReflection.Internal;

namespace SimpleReflection
{
#nullable enable
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
            ErrorHelper.ThrowArgumentNullException(methodName, nameof(methodName));

            return target!.GetType().GenerateAction(methodName);
        }

        public static ActionInvoker GenerateActionArgs(this Type target, string methodName, ArgsTypeArray argsTypeArray)
        {
            ErrorHelper.ThrowArgumentNullException(methodName, nameof(methodName));

            var m = MethodCreator.CreateInstance(target, methodName, argsTypeArray);
            return new ActionInvoker(m.method);
        }

        public static ActionInvoker GenerateActionArgs<T>(this T target, string methodName, ArgsTypeArray argsTypeArray)
        {
            return target!.GetType().GenerateActionArgs(methodName, argsTypeArray);
        }

        public static FunctionInvoker GenerateFunction(this Type target, string methodName)
        {
            ErrorHelper.ThrowArgumentNullException(methodName, nameof(methodName));

            var m = MethodCreator.CreateInstance(target, methodName);
            return new FunctionInvoker(m.method);
        }

        public static FunctionInvoker GenerateFunction<T>(this T target, string methodName)
        {
            return target!.GetType().GenerateFunction(methodName);
        }

        public static FunctionInvoker GenerateFunctionArgs(this Type target, string methodName, ArgsTypeArray argsTypeArray)
        {
            ErrorHelper.ThrowArgumentNullException(methodName, nameof(methodName));

            var m = MethodCreator.CreateInstance(target, methodName, argsTypeArray);
            return new FunctionInvoker(m.method);
        }

        public static FunctionInvoker GenerateFunctionArgs<T>(this T target, string methodName, ArgsTypeArray argsTypeArray)
        {
            return target!.GetType().GenerateFunctionArgs(methodName, argsTypeArray);
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

        public static GetPropertyInvoker GenerateGetProperty(this Type target, string propertyName)
        {
            ErrorHelper.ThrowArgumentNullException(propertyName, nameof(propertyName));

            var getProperty = PropertyCreator.CreateGetProperty(target, propertyName);
            return new GetPropertyInvoker(new FunctionInvoker(getProperty));
        }

        public static GetPropertyInvoker GenerateGetProperty<T>(this T target, string propertyName)
        {
            return target!.GetType().GenerateGetProperty(propertyName);
        }

        public static IndexedGetPropertyInvoker GenerateIndexedGetProperty<T>(this T target)
        {
            return target!.GetType().GenerateIndexedGetProperty();
        }

        public static IndexedGetPropertyInvoker GenerateIndexedGetProperty(this Type target)
        {
            var indexerName = IndexerExtractor.Extract(target);
            var getIndexer = PropertyCreator.CreateGetProperty(target, indexerName);
            return new IndexedGetPropertyInvoker(new FunctionInvoker(getIndexer));
        }

        public static IndexedSetPropertyInvoker GenerateIndexedSetProperty<T>(this T target)
        {
            return target!.GetType().GenerateIndexedSetProperty();
        }

        public static IndexedSetPropertyInvoker GenerateIndexedSetProperty(this Type target)
        {
            var indexerName = IndexerExtractor.Extract(target);
            var setIndexer = PropertyCreator.CreateSetProperty(target, indexerName);
            return new IndexedSetPropertyInvoker(new ActionInvoker(setIndexer));
        }

        public static SetPropertyInvoker GenerateSetProperty(this Type target, string propertyName)
        {
            ErrorHelper.ThrowArgumentNullException(propertyName, nameof(propertyName));

            var getProperty = PropertyCreator.CreateSetProperty(target, propertyName);
            return new SetPropertyInvoker(new ActionInvoker(getProperty));
        }

        public static SetPropertyInvoker GenerateSetProperty<T>(this T target, string propertyName)
        {
            return target!.GetType().GenerateSetProperty(propertyName);
        }

        public static StaticGetPropertyInvoker GenerateStaticGetProperty(this Type target, string propertyName)
        {
            ErrorHelper.ThrowArgumentNullException(propertyName, nameof(propertyName));

            var getProperty = PropertyCreator.CreateStaticGetProperty(target, propertyName);
            return new StaticGetPropertyInvoker(new StaticFunctionInvoker(getProperty));
        }

        public static StaticSetPropertyInvoker GenerateStaticSetProperty(this Type target, string propertyName)
        {
            ErrorHelper.ThrowArgumentNullException(propertyName, nameof(propertyName));

            var getProperty = PropertyCreator.CreateStaticSetProperty(target, propertyName);
            return new StaticSetPropertyInvoker(new StaticActionInvoker(getProperty));
        }

        public static object? InvokeGetProperty<T>(this T target, GetPropertyInvoker invoker)
        {
            ErrorHelper.ThrowArgumentNullException(invoker, nameof(invoker));

            return invoker.Invoke(target);
        }

        public static void InvokeSetProperty<T>(this T target, SetPropertyInvoker invoker, object? value)
        {
            ErrorHelper.ThrowArgumentNullException(invoker, nameof(invoker));

            invoker.Invoke(target, value);
        }

        public static object? InvokeIndexedGetProperty<T>(this T target, IndexedGetPropertyInvoker invoker, params object[]? indexerParameters)
        {
            ErrorHelper.ThrowArgumentNullException(invoker, nameof(invoker));
            ErrorHelper.ThrowArgumentNullException(indexerParameters, nameof(indexerParameters));
            ErrorHelper.ThrowNoElementInArray(indexerParameters!);

            return invoker.Invoke(target, indexerParameters);
        }

        public static void InvokeIndexedSetProperty<T>(this T target, IndexedSetPropertyInvoker invoker, object? value, params object[]? indexerParameters)
        {
            ErrorHelper.ThrowArgumentNullException(invoker, nameof(invoker));
            ErrorHelper.ThrowArgumentNullException(indexerParameters, nameof(indexerParameters));
            ErrorHelper.ThrowNoElementInArray(indexerParameters!);

            invoker.Invoke(target, value!, indexerParameters);
        }
    }
#nullable disable
}
