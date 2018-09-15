using NUnit.Framework;

namespace Scribe.Tests.NUnit
{
	public partial class TheFizzBuzzPlayer : Tests.TheFizzBuzzPlayer
	{
		protected override void AssertEquals(string expected, string actual)
		{
			Assert.AreEqual(expected, actual);
		}
	}

}