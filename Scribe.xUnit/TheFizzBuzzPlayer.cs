using Xunit;

namespace Scribe.Tests.xUnit
{
	public partial class TheFizzBuzzPlayer : Scribe.Tests.TheFizzBuzzPlayer
	{
		protected override void AssertEquals(string expected, string actual)
		{
			Assert.Equal(expected, actual);
		}
	}
}