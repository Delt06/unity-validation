using System;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Validation
{
    public static class ValidationExtensions
    {
        public static void Require<T>([NotNull] this GameObject context, out T component) where T : class
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (context.TryGetComponent(out component)) return;
            
            throw new ComponentValidationError(context, typeof(T));
        }
        
        public static void Require<T>([NotNull] this Component context, out T component) where T : class
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (context.TryGetComponent(out component)) return;
            
            throw new ComponentValidationError(context, typeof(T));
        }

        public static void RequireInParent<T>([NotNull] this GameObject context, out T component) where T : class
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            
            component = context.GetComponentInParent<T>();
            if (component != null) return;

            throw new ParentComponentValidationError(context, typeof(T));
        }
        
        public static void RequireInParent<T>([NotNull] this Component context, out T component) where T : class
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            
            component = context.GetComponentInParent<T>();
            if (component != null) return;

            throw new ParentComponentValidationError(context, typeof(T));
        }

        public static void RequireInChildren<T>([NotNull] this GameObject context, out T component) where T : class
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            
            component = context.GetComponentInChildren<T>();
            if (component != null) return;

            throw new ChildrenComponentValidationError(context, typeof(T));
        }
        
        public static void RequireInChildren<T>([NotNull] this Component context, out T component) where T : class
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            
            component = context.GetComponentInChildren<T>();
            if (component != null) return;

            throw new ChildrenComponentValidationError(context, typeof(T));
        }

        public static void RequireAnywhere<T>([NotNull] this Object context, out T component) where T : Object
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            
            component = Object.FindObjectOfType<T>();
            if (component != null) return;

            throw new AnywhereComponentValidationError(typeof(T), context);
        }
        
        public static void RequireAnywhere<T>(out T component) where T : Object
        {
            component = Object.FindObjectOfType<T>();
            if (component != null) return;

            throw new AnywhereComponentValidationError(typeof(T));
        }
    }
}