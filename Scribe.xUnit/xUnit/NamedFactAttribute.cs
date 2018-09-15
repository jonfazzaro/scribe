using System.Runtime.CompilerServices;
using Xunit;

namespace Scribe.xUnit
{
	public sealed class NamedFactAttribute : FactAttribute
	{
		public NamedFactAttribute([CallerMemberName] string name = null)
		{
			DisplayName = name;
		}
	}
}
