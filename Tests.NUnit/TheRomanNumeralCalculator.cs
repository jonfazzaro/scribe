using Scribe;
using NUnit.Framework;

namespace Tests.NUnit.Romans
{
	public partial class TheRomanNumeralCalculator : Tests.Romans.TheRomanNumeralCalculator
	{
		protected override void AssertEquals(string expected, string actual)
		{
			Assert.AreEqual(expected, actual);
		}
	}
}
