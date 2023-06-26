using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public interface ISearchMethods
    {
        IEnumerable<MethodDeclarationSyntax> AllMethods(INamedTypeSymbol classSymbol);
    }
}
