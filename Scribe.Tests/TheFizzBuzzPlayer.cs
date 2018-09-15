using FakeItEasy;

namespace Scribe.Tests
{
	public abstract partial class TheFizzBuzzPlayer : Spec
	{
		public TheFizzBuzzPlayer()
		{
			BeforeEach(() =>
			{
				_player = new FizzBuzzPlayer(A.Fake<ILogger>());
			});

			ExpectResult("1", 1);
			ExpectResult("2", 2);
			ExpectResult("fizz", 3);
			ExpectResult("buzz", 5);
			ExpectResult("fizz", 9);
			ExpectResult("buzz", 10);
			ExpectResult("fizzbuzz", 15);
			ExpectResult("fizzbuzz", 90);

			Describe("when playing", () =>
			{
				ILogger _logger = null;
				BeforeEach(() =>
				{
					_logger = A.Fake<ILogger>();
					_player = new FizzBuzzPlayer(_logger);
				});

				Describe("a 5", () =>
				{
					BeforeEach(() =>
					{
						_player.Play(5);
					});

					It("logs the play", () =>
					{
						A.CallTo(() => _logger.Log("The fizz-buzz of 5 is buzz.")).MustHaveHappened();
					});
				});

				Describe("a 7", () =>
				{
					BeforeEach(() =>
					{
						_player.Play(7);
					});

					It("logs the play", () =>
					{
						A.CallTo(() => _logger.Log("The fizz-buzz of 7 is 7.")).MustHaveHappened();
					});
				});
			});
		}

		FizzBuzzPlayer _player;
		void ExpectResult(string result, int input)
		{
			Describe("given " + input.ToString(), () =>
			{
				It("returns " + result, () =>
				{
					AssertEquals(result, _player.Play(input));
				});
			});
		}

		protected abstract void AssertEquals(string expected, string actual);
	}

	public interface ILogger
	{
		void Log(string v);
	}

	public class FizzBuzzPlayer
	{
		private ILogger _logger;

		public FizzBuzzPlayer(ILogger logger)
		{
			_logger = logger;
		}

		public string Play(int value)
		{
			var result = value.ToString();

			if (value % 3 == 0)
				result = "fizz";
			if (value % 5 == 0)
				result = "buzz";
			if (value % 15 == 0)
				result = "fizzbuzz";

			_logger.Log($"The fizz-buzz of {value} is {result}.");
			return result;
		}
	}
}