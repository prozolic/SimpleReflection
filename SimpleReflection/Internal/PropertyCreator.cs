﻿using System;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Reflection;

namespace SimpleReflection.Internal
{
    internal class PropertyCreator
    {
        public static object CreateGetProperty(Type target, string propertyName)
        {
            var propertyInfo = target.GetProperty(propertyName,
                BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.GetProperty);

            ErrorHelper.ThrowNullReferenceException(propertyInfo, "Failed to get property information.");

            return BuildPropertyMethod(target, propertyInfo?.GetGetMethod(true));
        }

        public static object CreateStaticGetProperty(Type target, string propertyName)
        {
            var propertyInfo = target.GetProperty(propertyName,
                BindingFlags.Static |
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.GetProperty);

            ErrorHelper.ThrowNullReferenceException(propertyInfo, "Failed to get property information.");

            return BuildStaticPropertyMethod(propertyInfo?.GetGetMethod(true));
        }

        public static object CreateSetProperty(Type target, string propertyName)
        {
            var propertyInfo = target.GetProperty(propertyName,
                BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.SetProperty);

            ErrorHelper.ThrowNullReferenceException(propertyInfo, "Failed to set property information.");

            return BuildPropertyMethod(target, propertyInfo?.GetSetMethod(true));
        }

        public static object CreateStaticSetProperty(Type target, string propertyName)
        {
            var propertyInfo = target.GetProperty(propertyName,
                BindingFlags.Static |
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.SetProperty);

            ErrorHelper.ThrowNullReferenceException(propertyInfo, "Failed to get property information.");

            return BuildStaticPropertyMethod(propertyInfo?.GetSetMethod(true));
        }

        private static object BuildPropertyMethod(Type target, MethodInfo? propertyInfo)
        {
            var m = MethodCreator.BuildMethod(target, propertyInfo);
            return m.method;
        }

        private static object BuildStaticPropertyMethod(MethodInfo? propertyInfo)
        {
            var m = MethodCreator.BuildStaticMethod(propertyInfo);
            return m.method;
        }

    }
}
