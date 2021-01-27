using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Scribe
{
	public class SpecReceiver : ISyntaxReceiver
	{
		public IList<ClassDeclarationSyntax> SpecClasses { get; } = new List<ClassDeclarationSyntax>();

		public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
		{
			if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax)
			{
				foreach (var type in classDeclarationSyntax.BaseList.Types)
				{
					if (typeof(Spec).IsAssignableFrom(type.GetType()))
						SpecClasses.Add(classDeclarationSyntax);
				}
			}
		}
	}
}
