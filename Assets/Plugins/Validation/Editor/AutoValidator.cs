using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Validation.Editor
{
	public static class AutoValidator
	{
		[MenuItem("Dependencies/Validate"), InitializeOnEnterPlayMode]
		public static void Run()
		{
			var objects = Object.FindObjectsOfType<Component>();
			
			var invalid = new List<Component>();

			foreach (var context in objects)
			{
				var type = context.GetType();
				var fields = type.GetDependencyFields();

				foreach (var field in fields)
				{
					var attribute = field.GetCustomAttribute<DependencyAttribute>();
					
					if (!ComponentIsAttached(context, attribute.Source, field.FieldType))
						invalid.Add(context);
				}
			}

			var invalidCount = invalid
				.Distinct()
				.Count();

			if (invalidCount == 0)
				Debug.Log("Validation passed successfully.");
			else
				Debug.LogError($"Validation failed. {invalidCount} invalid components were detected.");
		}

		private static bool ComponentIsAttached(Component context, Source source, Type type)
		{
			switch (source)
			{
				case Source.Local: return context.GetComponent(type) != null;
				case Source.FromParents: return context.GetComponentInParent(type) != null;
				case Source.FromChildren: return context.GetComponentsInChildren(type) != null;
				case Source.Global when typeof(Object).IsAssignableFrom(type): return Object.FindObjectsOfType(type) != null;
				case Source.Global: throw ValidationExtensions.NewGlobalDependencyIllegalTypeException(type, context);
				default: throw new ArgumentOutOfRangeException();
			}
		}
	}
}