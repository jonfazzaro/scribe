using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace Scribe.MSTest
{
	[Generator]
	public class MSTestGenerator : ISourceGenerator
	{

		public void Initialize(GeneratorInitializationContext context)
		{
			context.RegisterForSyntaxNotifications(() => new ClassReceiver());
		}

		public void Execute(GeneratorExecutionContext context)
		{
			if (!(context.SyntaxReceiver is ClassReceiver receiver))
				return;

			var code = new StringBuilder();
			foreach (var spec in receiver.Classes)
			{
				var classSymbol = ClassSymbol(context.Compilation, spec);
				if (classSymbol.BaseType.Name == "Spec")
				{
					code.Append($@"
							namespace {classSymbol.ContainingNamespace.Name}
							{{
								[TestClass]
								public partial class {classSymbol.Name}
								{{
						");

					//	//foreach (var test in spec.Tests)
					//	//	code.Append($"[TestMethod] public void {scribe.TestMethodName(test)}() => Run({test.Id});");

					code.AppendLine("}}");
					code.AppendLine("}}");
				}

			}

			context.AddSource($"Scribe.spec.cs",
				SourceText.From(code.ToString(), Encoding.UTF8));

		}

		static ITypeSymbol ClassSymbol(Compilation compilation, ClassDeclarationSyntax classDeclaration)
		{
			var model = compilation.GetSemanticModel(classDeclaration.SyntaxTree);
			return model.GetDeclaredSymbol(classDeclaration) as ITypeSymbol;
		}
	}
}
