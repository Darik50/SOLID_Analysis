using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public interface IMetricsCalculatorISP
    {
        int GetInterfaceMethodsCount(INamedTypeSymbol interfaceSymbol);
        double CalculateISP(INamedTypeSymbol classSymbol);
        int CountClassesImplementingInterface(INamedTypeSymbol interfaceSymbol, IEnumerable<INamedTypeSymbol> classes);
    }
}
