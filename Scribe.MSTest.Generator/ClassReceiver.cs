using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Scribe
{
	public class ClassReceiver : ISyntaxReceiver
	{
		public IList<ClassDeclarationSyntax> Classes { get; }
			= new List<ClassDeclarationSyntax>();

		public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
		{
			if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax)
				Classes.Add(classDeclarationSyntax);
		}
	}
}
