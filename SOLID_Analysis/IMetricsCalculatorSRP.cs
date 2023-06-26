using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public interface IMetricsCalculatorSRP
    {
        int CountMethods(INamedTypeSymbol classSymbol);
        int CountParameterTypesClasses(INamedTypeSymbol classSymbol);
        int CountParameterTypesMethods(MethodDeclarationSyntax method);
        int GetNumberOfLines(INamedTypeSymbol classSymbol);
        int CountResponsibilities(INamedTypeSymbol classSymbol);
    }
}
