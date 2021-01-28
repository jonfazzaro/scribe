using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scribe.MSTest;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Scribe.Tests
{
	[TestClass]
	public class TheMSTestGenerator
	{
		const string source = @"
namespace Scribe.Tests.TestCode 
{
	public class MySpec : Scribe.Spec { }
	public class MyOtherSpec : Scribe.Spec { }
	public class NotASpec { }
}
			";

		[TestMethod]
		public void GeneratesOutput()
		{
			var output = Compile(source);
			Assert.AreEqual(2, output.SyntaxTrees.Count());
		}

		[TestMethod]
		public void GeneratesAClassForEachSpec()
		{
			var output = Compile(source);
			var root = output.SyntaxTrees.Last().GetCompilationUnitRoot();

			var classes = root.DescendantNodes().Where(n => n.Kind() == SyntaxKind.ClassDeclaration);

			var classSymbols = classes.Select(c => ClassSymbol(output, c as ClassDeclarationSyntax));
			CollectionAssert.AreEqual(
				new List<string> { "MySpec", "MyOtherSpec" },
				classSymbols.Select(c => c.Name).ToList());
		}

		static ISymbol ClassSymbol(Compilation compilation, ClassDeclarationSyntax classDeclaration)
		{
			var model = compilation.GetSemanticModel(classDeclaration.SyntaxTree);
			return model.GetDeclaredSymbol(classDeclaration);
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
