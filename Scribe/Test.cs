using System;
using System.Collections.Generic;

namespace Scribe
{
	public class Test
	{
		public int Id { get; set; }
		public string Context { get; set; }
		public string Name { get; set; }
		public IEnumerable<Action> Arrangements { get; set; }
		public Action Body { get; set; }

		public string FullName =>
			string.IsNullOrWhiteSpace(Context) ? Name : string.Join(" ", Context, Name);

		public void Run() {
			foreach (var arrangement in Arrangements)
				arrangement();
			Body();
		}
	}

}
