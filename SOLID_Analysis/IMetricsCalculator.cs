using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public interface IMetricsCalculator
    {
        int CountClasses(Project project);
        int CountMethods(INamedTypeSymbol classSymbol);
        int CountUsedMethods(INamedTypeSymbol classSymbol);
        List<MethodDeclarationSyntax> GetCalledMethods(INamedTypeSymbol classSymbol, List<INamedTypeSymbol> allClasses);
        int CountParameters(INamedTypeSymbol classSymbol);
        int CountStaticFieldsAndProperties(INamedTypeSymbol classSymbol);
        //Dictionary<string, int> DictionaryParameterTypesClasses(INamedTypeSymbol classSymbol);
        //Dictionary<string, int> DictionaryParameterTypesMethods(MethodDeclarationSyntax method);
        int CountParameterTypesClasses(INamedTypeSymbol classSymbol);
        int CountParameterTypesMethods(MethodDeclarationSyntax method);
        List<INamedTypeSymbol> InhClassAsync(IEnumerable<INamedTypeSymbol> classes);
        List<INamedTypeSymbol> RootClassAsync(IEnumerable<INamedTypeSymbol> classes);
        List<INamedTypeSymbol> GetAllDependentClasses(INamedTypeSymbol classSymbol);
        IEnumerable<INamedTypeSymbol> GetAllDerivedClasses(INamedTypeSymbol symbol, Solution solution);
    }
}
