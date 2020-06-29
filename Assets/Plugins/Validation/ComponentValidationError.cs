using System;
using Object = UnityEngine.Object;

namespace Validation
{
    public sealed class ComponentValidationError : Exception
    {
        public override string Message { get; }

        public ComponentValidationError(Object context, Type componentType)
        {
            Message = $"Component of type {componentType.Name} was not found in {context.name}.";
        }
    }
}