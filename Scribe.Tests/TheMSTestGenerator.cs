using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scribe.MSTest;
using System.Linq;
using System.Reflection;

namespace Scribe.Tests
{
	[TestClass]
	public class TheMSTestGenerator
	{
		[TestMethod]
		public void GivenOneSpec_GeneratesOneTree()
		{
			const string source = @"
namespace Scribe.Tests.TestCode 
{
	public class MySpec : Scribe.Spec
	{
		public MySpec() 
		{
		}
	}
}
			";
			var output = Compile(source);

			Assert.AreEqual(1, output.SyntaxTrees.Count());

		}

		static Compilation Compile(string source)
		{
			var compilation = CreateCompilation(source);
			var subject = new MSTestGenerator();
			GeneratorDriver driver = CSharpGeneratorDriver.Create(subject);
			driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var diagnostics);
			return outputCompilation;
		}

		static Compilation CreateCompilation(string source)
			=> CSharpCompilation.Create("compilation",
				new[] { CSharpSyntaxTree.ParseText(source) },
				new[] { MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location) },
				new CSharpCompilationOptions(OutputKind.ConsoleApplication));
	}
}
