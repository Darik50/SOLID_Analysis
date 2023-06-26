using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public interface IMetricsCalculatorLSP
    {
        int GetInheritanceDepth(INamedTypeSymbol symbol);
        IEnumerable<INamedTypeSymbol> GetDependentClasses(INamedTypeSymbol classSymbol);
        int CountMethods(INamedTypeSymbol classSymbol);
        List<MethodDeclarationSyntax> GetCalledMethods(INamedTypeSymbol classSymbol, List<INamedTypeSymbol> allClasses);
    }
}
