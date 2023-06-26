using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public interface ISearchCalsses
    {
        Task<IEnumerable<INamedTypeSymbol>> AllClassAsync(Project project);
        List<INamedTypeSymbol> BaseClass(IEnumerable<INamedTypeSymbol> classes, Project project);
        List<INamedTypeSymbol> InhClassAsync(IEnumerable<INamedTypeSymbol> classes);
        List<INamedTypeSymbol> GetAllInterfaces(Project project);
        List<INamedTypeSymbol> GetAbstractClasses(Project project);

    }
}
