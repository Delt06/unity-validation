using System;

namespace Validation
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class RequireComponentInParent : Attribute
	{
		public Type Type { get; }

		public RequireComponentInParent(Type type) => Type = type;
	}
}