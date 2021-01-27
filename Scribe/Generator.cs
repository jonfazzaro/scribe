using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Scribe
{
	public class Generator
	{
		public string TestName(Test test)
		{
			return string.Join(" ", test.Context, test.Name);
		}

		public string TestMethodName(Test test)
		{
			var textInfo = new CultureInfo("en-US", false).TextInfo;
			return textInfo.ToTitleCase(string.Join("_", test.Context, test.Name).ToLower())
				.Replace(" ", string.Empty)
				.Replace("'", string.Empty);
		}

		public IEnumerable<Spec> Specs()
		{
			try
			{
				var thisAssembly = ThisAssembly();
				if (thisAssembly is null)
					return Enumerable.Empty<Spec>();

				// TODO: handle AssemblyResolve event and scan the NuGet dir ($(NuGetPackageRoot))

				return thisAssembly.GetTypes()
					.Where(t =>
						t != typeof(Spec) &&
						typeof(Spec).IsAssignableFrom(t))
					.Select(t => Activator.CreateInstance(t) as Spec);
			}
			catch (Exception)
			{
				return Enumerable.Empty<Spec>();
			}
		}

		public Assembly ThisAssembly()
		{
			return Assembly.GetExecutingAssembly();
			//var path = Host.ResolveAssemblyReference("$(TargetPath)");
			//if (!File.Exists(path))
			//	return null;
			//return Assembly.LoadFrom(path);
		}
	}
}
