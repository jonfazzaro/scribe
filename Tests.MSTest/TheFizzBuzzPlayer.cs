using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scribe.Tests.MSTest
{
	public partial class TheFizzBuzzPlayer : Tests.TheFizzBuzzPlayer
	{
		protected override void AssertEquals(string expected, string actual)
		{
			Assert.AreEqual(expected, actual);
		}
	}
}