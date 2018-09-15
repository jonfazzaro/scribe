using Xunit;

namespace Tests.xUnit
{
	public partial class TheFizzBuzzPlayer : Tests.TheFizzBuzzPlayer
	{
		protected override void AssertEquals(string expected, string actual)
		{
			Assert.Equal(expected, actual);
		}
	}
}