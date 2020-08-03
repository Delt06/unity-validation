using System;

namespace Validation
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class RequireComponentAnywhere : Attribute
	{
		public Type Type { get; }

		public RequireComponentAnywhere(Type type) => Type = type;
	}
}