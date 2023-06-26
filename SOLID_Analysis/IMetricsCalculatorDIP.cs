using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public interface IMetricsCalculatorDIP
    {
        List<INamedTypeSymbol> GetAllDependentClasses(INamedTypeSymbol classSymbol);
        int CountInheritors(INamedTypeSymbol classSymbol, Project project);
        int CountAbstractAndInterfaceUsages(INamedTypeSymbol classSymbol);
    }
}
