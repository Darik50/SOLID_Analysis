using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public interface IMetricsCalculatorOCP
    {
        IEnumerable<INamedTypeSymbol> GetDependentClasses(INamedTypeSymbol classSymbol);
        int GetOverriddenMethodCount(INamedTypeSymbol classSymbol);
        int GetInheritanceDepth(INamedTypeSymbol symbol);
    }
}
