using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public class SearchCalsses : ISearchCalsses
    {
        //список классов
        public async Task<IEnumerable<INamedTypeSymbol>>
            AllClassAsync(Project project)
        {
            Compilation compilation = await project
                .GetCompilationAsync();
            IEnumerable<INamedTypeSymbol> classes = 
                compilation.GlobalNamespace.GetNamespaceMembers()
                .SelectMany(x => x.GetTypeMembers());
            return classes;
        }
        //список не системных классов
        public List<INamedTypeSymbol> BaseClass
            (IEnumerable<INamedTypeSymbol> classes, Project project)
        {
            List<INamedTypeSymbol> names = 
                new List<INamedTypeSymbol>();
            foreach (var classSymbol in classes)
            {
                var baseType = classSymbol.BaseType;
                if (classSymbol.ContainingNamespace.ToString()
                    .Equals(project.Name) && !classSymbol
                    .ContainingNamespace.ToString()
                    .Equals("<CppImplementationDetails>") && 
                    !classSymbol.ContainingNamespace
                    .ToString()
                    .Equals("<CrtImplementationDetails>"))
                {
                    if (baseType != null && baseType.Name ==
                        "Object")
                    {
                        names.Add(classSymbol);
                    }
                }
            }
            return names;
        }
        //список интерфейсов
        public List<INamedTypeSymbol> GetAllInterfaces
            (Project project)
        {
            var interfaces = new List<INamedTypeSymbol>();
            var compilation = project
                .GetCompilationAsync().Result;
            foreach (var syntaxTree in compilation.SyntaxTrees)
            {
                var model = compilation
                    .GetSemanticModel(syntaxTree);
                var root = syntaxTree
                    .GetCompilationUnitRoot();
                var interfaceDeclarations = root
                    .DescendantNodes()
                    .OfType<InterfaceDeclarationSyntax>();
                foreach (var interfaceDeclaration in interfaceDeclarations)
                {
                    var symbol = model.GetDeclaredSymbol(interfaceDeclaration) as INamedTypeSymbol;
                    interfaces.Add(symbol);
                }
            }
            return interfaces;
        }
        //список абтрактных классов
        public List<INamedTypeSymbol> GetAbstractClasses
            (Project project)
        {
            List<INamedTypeSymbol> abstractClasses
                = new List<INamedTypeSymbol>();
            ISearchCalsses searchClasses 
                = new SearchCalsses();
            var classes = searchClasses
                .AllClassAsync(project);
            foreach (var cls in classes.Result)
            {
                if (cls.IsAbstract)
                {
                    abstractClasses.Add(cls);
                }
            }
            return abstractClasses;
        }
        //список наследуемых классов
        public List<INamedTypeSymbol> InhClassAsync
            (IEnumerable<INamedTypeSymbol> classes)
        {
            List<INamedTypeSymbol> names 
                = new List<INamedTypeSymbol>();
            foreach (var classSymbol in classes)
            {
                var baseType = classSymbol.BaseType;
                if (!classSymbol.ContainingNamespace
                    .ToString().Equals("System") && 
                    !classSymbol.ContainingNamespace
                    .ToString()
                    .Equals("<CppImplementationDetails>") 
                    && !classSymbol.ContainingNamespace
                    .ToString()
                    .Equals("<CrtImplementationDetails>"))
                {
                    if (baseType != null && 
                        baseType.Name != "Object")
                    {
                        names.Add(classSymbol);
                    }
                }
            }
            return names;
        }
    }
}
