using System;

namespace Validation
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class DependencyAttribute : Attribute
	{
		public Source Source { get; }

		public DependencyAttribute(Source source = Source.Local) => Source = source;
	}
}