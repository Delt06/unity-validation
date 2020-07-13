using System;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace Validation
{
    public sealed class SceneComponentValidationError : ComponentValidationErrorBase
    {
        public SceneComponentValidationError(Type componentType, [CanBeNull] Object context = null) : 
            base(FormatMessage(componentType, context))
        {
        }

        private static string FormatMessage(Type componentType, [CanBeNull] Object context = null)
        {
            var baseMessage = $"Component of type {componentType.Name} was not found on the scene";

            if (context != null)
            {
                baseMessage += $" (called from {context.name})";
            }
            
            baseMessage += ".";

            return baseMessage;
        }
     }
}