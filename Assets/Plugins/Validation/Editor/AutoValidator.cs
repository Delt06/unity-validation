using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Validation.Editor
{
	public static class AutoValidator
	{
		[MenuItem("Auto Validator/Run"), InitializeOnEnterPlayMode]
		public static void Run()
		{
			var objects = Object.FindObjectsOfType<Object>();

			objects.Validate(out var invalid);
			objects.ValidateInParents(out var invalidInParents);
			objects.ValidateInChildren(out var invalidInChildren);
			objects.ValidateAnywhere(out var invalidAnywhere);

			var invalidCount = invalid
				.Concat(invalidInParents)
				.Concat(invalidInChildren)
				.Concat(invalidAnywhere)
				.Distinct()
				.Count();

			if (invalidCount == 0)
				Debug.Log("Validation passed successfully.");
			else
				Debug.LogError($"Validation failed. {invalidCount} invalid components were detected.");
		}

		private static void Validate(this IEnumerable<Object> objects, out IEnumerable<Object> invalid)
		{
			var invalidList = new List<Object>();

			foreach (var obj in objects.OfType<Component>())
			{
				var attributes = Attribute.GetCustomAttributes(obj.GetType())
					.OfType<RequireComponent>().ToArray();
				if (attributes.Length == 0) continue;

				foreach (var attribute in attributes)
				{
					var types = new[] {attribute.m_Type0, attribute.m_Type1, attribute.m_Type2}
						.Where(a => a != null);

					foreach (var type in types)
					{
						if (!obj.TryGetComponent(type, out _))
						{
							Debug.LogError($"{obj} lacks {type} on it.", obj);
							invalidList.Add(obj);
						}
					}
				}
			}

			invalid = invalidList.Distinct();
		}

		private static void ValidateInParents(this IEnumerable<Object> objects, out IEnumerable<Object> invalid)
		{
			var invalidList = new List<Object>();

			foreach (var obj in objects.OfType<Component>())
			{
				var attributes = Attribute.GetCustomAttributes(obj.GetType())
					.OfType<RequireComponentInParent>().ToArray();
				if (attributes.Length == 0) continue;

				foreach (var attribute in attributes)
				{
					var type = attribute.Type;
					if (type == null) continue;

					var component = obj.GetComponentInParent(type);
					if (component == null)
					{
						Debug.LogError($"{obj} lacks {type} on it or its parent.", obj);
						invalidList.Add(obj);
					}
				}
			}

			invalid = invalidList.Distinct();
		}

		private static void ValidateInChildren(this IEnumerable<Object> objects, out IEnumerable<Object> invalid)
		{
			var invalidList = new List<Object>();

			foreach (var obj in objects.OfType<Component>())
			{
				var attributes = Attribute.GetCustomAttributes(obj.GetType())
					.OfType<RequireComponentInChildren>().ToArray();
				if (attributes.Length == 0) continue;

				foreach (var attribute in attributes)
				{
					var type = attribute.Type;
					if (type == null) continue;

					var component = obj.GetComponentInChildren(type);
					if (component == null)
					{
						Debug.LogError($"{obj} lacks {type} on it or its children.", obj);
						invalidList.Add(obj);
					}
				}
			}

			invalid = invalidList.Distinct();
		}

		private static void ValidateAnywhere(this IEnumerable<Object> objects, out IEnumerable<Object> invalid)
		{
			var invalidList = new List<Object>();

			foreach (var obj in objects.OfType<Component>())
			{
				var attributes = Attribute.GetCustomAttributes(obj.GetType())
					.OfType<RequireComponentAnywhere>().ToArray();
				if (attributes.Length == 0) continue;

				foreach (var attribute in attributes)
				{
					var type = attribute.Type;
					if (type == null) continue;

					var component = Object.FindObjectOfType(type);
					if (component == null)
					{
						Debug.LogError($"{obj} lacks {type} anywhere.", obj);
						invalidList.Add(obj);
					}
				}
			}

			invalid = invalidList.Distinct();
		}
	}
}