namespace Scribe.Tests.Romans
{
	public abstract partial class TheRomanNumeralCalculator : Spec
	{
		RomanNumeralCalculator _calculator;
		public TheRomanNumeralCalculator()
		{
			BeforeEach(() =>
			{
				_calculator = new RomanNumeralCalculator();
			});

			ExpectAddingResult("II", "I", "I");
			ExpectAddingResult("III", "I", "II");
			ExpectAddingResult("IV", "I", "III");

		}

		void ExpectAddingResult(string result, string n1, string n2)
		{
			Describe($"when adding {n1}", () =>
			{
				Describe($"and {n2}", () =>
				{
					var sum = string.Empty;
					BeforeEach(() => {
						sum = _calculator.Add(n1, n2);
					});

					It($"returns {result}", () =>
					{
						AssertEquals(result, sum);
					});
				});
			});
		}

		protected abstract void AssertEquals(string expected, string actual);
	}

	public class RomanNumeralCalculator
	{
		public string Add(string v1, string v2)
		{
			var result = v1 + v2;
			result = result.Replace("IIII", "IV");

			return result;
		}
	}
}
