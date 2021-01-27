using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scribe.Tests.MSTest.Romans
{
	public partial class TheRomanNumeralCalculator : Tests.Romans.TheRomanNumeralCalculator
	{
		protected override void AssertEquals(string expected, string actual)
		{
			Assert.AreEqual(expected, actual);
		}
	}
}
