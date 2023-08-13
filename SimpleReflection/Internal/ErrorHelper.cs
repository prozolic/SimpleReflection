using System;
using System.Runtime.CompilerServices;

namespace SimpleReflection.Internal
{
    internal static class ErrorHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowNullReferenceException<T>(T value, string errorText)
            where T : class?
        {
            if (value == null) throw new NullReferenceException(errorText);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowArgumentNullException<T>(T value, string errorText)
            where T : class?
        {
            if (value == null) throw new ArgumentNullException(errorText);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowNoElementInArray<T>(T[] value)
        {
            if (value.Length == 0) throw new InvalidOperationException("There is no element of anything.");
        }


    }
}
