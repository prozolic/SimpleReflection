using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleReflection
{
    public struct ArgsTypeArray : IReadOnlyCollection<Type>
    {
        private readonly Type[] argsTypes;

        public static readonly ArgsTypeArray Empty = new (System.Array.Empty<Type>());

        public Type[] Array => argsTypes;

        public int Count => argsTypes.Length;

        public ArgsTypeArray(Type[] argsTypes)
        {
            this.argsTypes = argsTypes ?? Empty.Array;
        }

        public IEnumerator<Type> GetEnumerator()
        {
            for (int i = 0; i < argsTypes.Length; i++)
            {
                yield return argsTypes[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return argsTypes.GetEnumerator();
        }
    }
}
