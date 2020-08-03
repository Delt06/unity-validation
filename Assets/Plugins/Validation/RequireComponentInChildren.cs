using System;
using UnityEngine;

namespace Validation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequireComponentInChildren : Attribute
    {
        public Type Type { get; }

        public RequireComponentInChildren(Type type)
        {
            Type = type;
        }
    }
}