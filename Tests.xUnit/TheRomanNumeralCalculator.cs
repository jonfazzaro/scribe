using Xunit;

namespace Scribe.Tests.xUnit.Romans
{
	public partial class TheRomanNumeralCalculator : Tests.Romans.TheRomanNumeralCalculator
	{
		protected override void AssertEquals(string expected, string actual)
		{
			Assert.Equal(expected, actual);
		}
	}
}
