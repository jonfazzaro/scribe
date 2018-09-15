using System;
using System.Collections.Generic;
using System.Linq;

namespace Scribe
{
	public abstract class Spec
	{
		public Spec()
		{
			Tests = new List<Test>();
			_captions = new Stack<string>();
			_arrangements = new Stack<Action>();
		}

		protected Stack<string> _captions;
		protected void Describe(string caption, Action body)
		{
			var originalArrangementsCount = _arrangements.Count();
			_captions.Push(caption);
			body();
			ResetCaptions();
			ResetArrangements(originalArrangementsCount);
		}

		protected Stack<Action> _arrangements;
		protected void BeforeEach(Action body)
		{
			_arrangements.Push(body);
		}

		int _lastId;
		public ICollection<Test> Tests { get; private set; }
		protected void It(string caption, Action body)
		{
			Tests.Add(new Test
			{
				Id = _lastId++,
				Name = caption,
				Context = DescribeContext(),
				Arrangements = CurrentArrangements(),
				Body = body
			});
		}

		protected void Run(int id) => Tests.FirstOrDefault(t => t.Id == id).Run();

		IEnumerable<Action> CurrentArrangements()
		{
			return _arrangements.Reverse().ToList();
		}

		string DescribeContext()
		{
			return string.Join(" ", _captions.Reverse());
		}

		void ResetArrangements(int originalArrangementsCount)
		{
			while (originalArrangementsCount < _arrangements.Count())
				_arrangements.Pop();
		}

		void ResetCaptions()
		{
			if (_captions.Any())
				_captions.Pop();
		}

	}

}
