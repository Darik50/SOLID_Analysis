using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FindSymbols;
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
    public class MetricsCalculator : IMetricsCalculator, 
        IMetricsCalculatorSRP, IMetricsCalculatorOCP, 
        IMetricsCalculatorLSP, IMetricsCalculatorISP, 
        IMetricsCalculatorDIP
    {
        //количество классов
        public int CountClasses(Project project)
        {
            int classCount = 0;
            ISearchCalsses searchCalsses = new SearchCalsses();
            var allClass = searchCalsses.AllClassAsync(project);
            classCount = allClass.Result.Count();
            return classCount;
        }
        //количество методов
        public int CountMethods(INamedTypeSymbol classSymbol)
        {
            int methodCount = 0;
            ISearchMethods searchMethods = new SearchMethods();
            var allMethod = searchMethods.AllMethods(classSymbol);
            methodCount = allMethod.Count();
            return methodCount;
        }
        //количество использованных методов внутри этого класса
        public int CountUsedMethods(INamedTypeSymbol classSymbol)
        {
            string text = classSymbol.DeclaringSyntaxReferences
                .First().GetSyntax().GetText().ToString();
            SyntaxTree tree = CSharpSyntaxTree.ParseText(text);
            SyntaxNode root = tree.GetRoot();
            var methodCalls = root.DescendantNodes()
                .OfType<InvocationExpressionSyntax>()
                .Select(invocation => invocation.Expression)
                .OfType<MemberAccessExpressionSyntax>()
                .Select(memberAccess => memberAccess.Name
                .Identifier.ValueText)
                .ToList();
            return methodCalls.Count;
        }
        //список методов которые вызывают другие классы
        public List<MethodDeclarationSyntax> GetCalledMethods
            (INamedTypeSymbol classSymbol,
            List<INamedTypeSymbol> allClasses)
        {
            List<MethodDeclarationSyntax> callingMethods 
                = new List<MethodDeclarationSyntax>();
            ISearchMethods searchMethods = new SearchMethods();
            var methods = searchMethods.AllMethods(classSymbol)
                .ToList();
            foreach (var method in methods)
            {
                var methodCalls = method.DescendantNodes()
                    .OfType<InvocationExpressionSyntax>();
                foreach (var methodCall in methodCalls)
                {
                    var symbol = classSymbol.ContainingAssembly
                        .GetTypeByMetadataName(methodCall
                        .Expression.ToString());
                    if (symbol != null)
                    {
                        INamedTypeSymbol containingType = symbol
                            .ContainingType;

                        if (containingType != null && 
                            !containingType
                            .ToString().StartsWith("System"))
                        {
                            if (allClasses.Contains(containingType))
                            {
                                callingMethods.Add(method);
                            }
                        }
                    }
                }
            }
            return callingMethods;
        }
        //количесвто параметров в классе включая параметры в методах
        public int CountParameters(INamedTypeSymbol classSymbol)
        {
            int parameterCount = 0;
            foreach (var member in classSymbol.GetMembers())
            {
                if (member.Kind == SymbolKind.Method)
                {
                    var methodSymbol = (IMethodSymbol)member;
                    parameterCount += methodSymbol.Parameters
                        .Length;
                }
            }
            return parameterCount;
        }
        //количесвто параметров в классе 
        public int CountStaticFieldsAndProperties
            (INamedTypeSymbol classSymbol)
        {
            int count = 0;
            foreach (var member in classSymbol.GetMembers())
            {
                if (member.Kind == SymbolKind.Field)
                {
                    var fieldSymbol = (IFieldSymbol)member;
                    if (fieldSymbol.IsStatic)
                    {
                        count++;
                    }
                }
                else if (member.Kind == SymbolKind.Property)
                {
                    var propertySymbol = (IPropertySymbol)member;
                    if (propertySymbol.IsStatic)
                    {
                        count++;
                    }
                }
            }
            return count;
        }        
        //количество типов данных во всем классе 
        public int CountParameterTypesClasses
            (INamedTypeSymbol classSymbol)
        {
            ISearchMethods searchMethods = new SearchMethods();
            var methods = searchMethods.AllMethods(classSymbol);
            var typeSymbols = methods
                .SelectMany(method => method.ParameterList
                .Parameters.Select(parameter => parameter.Type))
                .Distinct()
                .ToList();
            return typeSymbols.Count;
        }
        //количество типов данных в методе
        public int CountParameterTypesMethods
            (MethodDeclarationSyntax method)
        {
            var parameterTypes = method.ParameterList.Parameters
                .Select(parameter => parameter.Type)
                .Distinct()
                .Count();
            return parameterTypes;
        }
        //список наследуемых классов
        public List<INamedTypeSymbol> InhClassAsync
            (IEnumerable<INamedTypeSymbol> classes)
        {
            List<INamedTypeSymbol> names = 
                new List<INamedTypeSymbol>();
            foreach (var classSymbol in classes)
            {
                var baseType = classSymbol.BaseType;
                if (!classSymbol.ContainingNamespace
                    .ToString().Equals("System") && 
                    !classSymbol.ContainingNamespace
                    .ToString()
                    .Equals("<CppImplementationDetails>") && 
                    !classSymbol.ContainingNamespace.ToString()
                    .Equals("<CrtImplementationDetails>"))
                {
                    if (baseType != null && baseType.Name != 
                        "Object")
                    {
                        names.Add(classSymbol);
                    }
                }
            }
            return names;
        }
        //список корневых классов
        public List<INamedTypeSymbol> RootClassAsync
            (IEnumerable<INamedTypeSymbol> classes)
        {
            List<INamedTypeSymbol> names = 
                new List<INamedTypeSymbol>();
            foreach (var classSymbol in classes)
            {
                var baseType = classSymbol.BaseType;
                if (!classSymbol.ContainingNamespace.ToString()
                    .Equals("System") && !classSymbol.
                    ContainingNamespace.ToString().
                    Equals("<CppImplementationDetails>") && 
                    !classSymbol.ContainingNamespace.ToString()
                    .Equals("<CrtImplementationDetails>"))
                {
                    if (baseType.Name == "Object")
                    {
                        names.Add(classSymbol);
                    }
                }
            }
            return names;
        }
        //список завсисимых классов от выбранного класса
        public List<INamedTypeSymbol> GetAllDependentClasses
            (INamedTypeSymbol classSymbol)
        {
            var dependentClasses =
                new List<INamedTypeSymbol>();
            DFS(classSymbol, dependentClasses,
                new HashSet<INamedTypeSymbol>());
            return dependentClasses;
        }
        //Рекурсивно обходим все зависимые классы, и выписываем зависящие от них классы
        public void DFS(INamedTypeSymbol classSymbol,
            List<INamedTypeSymbol> dependentClasses,
            HashSet<INamedTypeSymbol> visited)
        {
            visited.Add(classSymbol);
            foreach (var dependentClass 
                in GetDependentClasses(classSymbol))
            {
                if (!visited.Contains(dependentClass))
                {
                    dependentClasses.Add(dependentClass);
                    DFS(dependentClass, 
                        dependentClasses, visited);
                }
            }
        }
        //Получаем все зависимые от входного класса классы
        public IEnumerable<INamedTypeSymbol> GetDependentClasses
            (INamedTypeSymbol classSymbol)
        {
            var dependentClasses = new List<INamedTypeSymbol>();
            var inheritance = classSymbol.AllInterfaces
                .Concat<INamedTypeSymbol>(classSymbol.BaseType !=
                null ? new[] { classSymbol.BaseType } : Enumerable
                .Empty<INamedTypeSymbol>());
            dependentClasses.AddRange(inheritance);
            dependentClasses.AddRange(classSymbol.GetMembers()
                .OfType<IFieldSymbol>()
                .Select(f => f.Type)
                .OfType<INamedTypeSymbol>());
            return dependentClasses;
        }
        //Получаем список дочерних классов
        public IEnumerable<INamedTypeSymbol> GetAllDerivedClasses
            (INamedTypeSymbol symbol, Solution solution)
        {
            var derivedTypes = SymbolFinder
                .FindDerivedClassesAsync(symbol, solution).Result;
            return derivedTypes.OfType<INamedTypeSymbol>();
        }
        //Получаем количество ответственностей класса
        public int CountResponsibilities
            (INamedTypeSymbol classSymbol)
        {
            int count = 0;
            foreach (var member in classSymbol.GetMembers())
            {
                if (member.Kind == SymbolKind.Method)
                {
                    var methodSymbol = (IMethodSymbol)member;
                    if (methodSymbol.MethodKind == MethodKind
                        .Ordinary)
                    {
                        count++;
                    }
                }
                else if (member.Kind == SymbolKind.Property ||
                    member.Kind == SymbolKind.Field)
                {
                    count++;
                }
            }
            return count;
        }
        //Получаем количество строк класса
        public int GetNumberOfLines(INamedTypeSymbol classSymbol)
        {
            var tree = classSymbol.DeclaringSyntaxReferences[0]
                .SyntaxTree;
            var classNode = tree.GetRoot().FindNode(classSymbol
                .DeclaringSyntaxReferences[0].Span) 
                as ClassDeclarationSyntax;
            return classNode.GetText().Lines.Count;
        }
        //Количество переопределенных методов
        public int GetOverriddenMethodCount
            (INamedTypeSymbol classSymbol)
        {
            int overriddenMethodsCount = 0;
            var methods = classSymbol.GetMembers()
                .OfType<IMethodSymbol>()
                .ToList();
            foreach (var method in methods)
            {
                if (method.IsOverride)
                {
                    overriddenMethodsCount++;
                }
            }
            return overriddenMethodsCount;
        }
        //Глубина иерархии наследования в системе 
        public int GetInheritanceDepth(INamedTypeSymbol symbol)
        {
            if (symbol.BaseType == null)
            {
                return 1;
            }
            else
            {
                return GetInheritanceDepth(symbol.BaseType) + 1;
            }
        }
        //Коэффициент разделения интерфейса
        public double CalculateISP(INamedTypeSymbol classSymbol)
        {
            var implementedInterfaces = classSymbol.AllInterfaces;
            var memberCount = 0;
            var interfaceMemberCount = 0;

            foreach (var implementedInterface in 
                implementedInterfaces)
            {
                foreach (var interfaceMember in 
                    implementedInterface.GetMembers())
                {
                    interfaceMemberCount++;

                    if (classSymbol
                        .FindImplementationForInterfaceMember
                        (interfaceMember) != null)
                    {
                        memberCount++;
                    }
                }
            }

            if (interfaceMemberCount == 0)
            {
                return 0;
            }

            return (double)memberCount / 
                (double)interfaceMemberCount;
        }
        //Количество методов в интерфейсе
        public int GetInterfaceMethodsCount
            (INamedTypeSymbol interfaceSymbol)
        {
            var methodCount = interfaceSymbol.GetMembers()
                                  .OfType<IMethodSymbol>()
                                  .Count();
            return methodCount;
        }
        //Количество классов, которые реализуют интерфейс
        public int CountClassesImplementingInterface
            (INamedTypeSymbol interfaceSymbol, 
            IEnumerable<INamedTypeSymbol> classes)
        {
            int count = 0;
            foreach (var type in classes)
            {
                if (type.TypeKind == TypeKind.Class &&
                    type.AllInterfaces
                    .Contains(interfaceSymbol))
                {
                    count++;
                }
            }
            return count;
        }
        //количество использований интерфейсов или абстрактных классов
        public int CountAbstractAndInterfaceUsages
            (INamedTypeSymbol classSymbol)
        {
            int count = 0;
            foreach (var member in classSymbol.GetMembers())
            {                if (member is IMethodSymbol method)
                {
                    if (method.ReturnType.TypeKind == TypeKind
                        .Interface ||
                        method.ReturnType.IsAbstract)
                        count++;
                    foreach (var param in method.Parameters)
                    {
                        if (param.Type.TypeKind == TypeKind
                            .Interface || 
                            param.Type.IsAbstract)
                            count++;
                    }
                }                else if 
                    (member is IPropertySymbol property)
                {                                
                    if (property.Type.TypeKind == 
                        TypeKind.Interface || 
                        property.Type.IsAbstract)
                        count++;
                }
            }            var baseType = classSymbol.BaseType;
            while (baseType != null)
            {
                count += CountAbstractAndInterfaceUsages(baseType);
                baseType = baseType.BaseType;
            }
            return count;
        }
        //Количество наследников интерфейса или абстрактного класса
        public int CountInheritors
            (INamedTypeSymbol classSymbol, Project project)
        {
            int count = 0;
            ISearchCalsses searchCalsses = 
                new SearchCalsses();
            var classes = searchCalsses
                .AllClassAsync(project);
            var baseClasses = searchCalsses
                .BaseClass(classes.Result, project);
            foreach (var type in baseClasses)
            {              
                if (type.BaseType?
                    .Equals(classSymbol) == true || type
                    .AllInterfaces.Contains(classSymbol))
                    count++;
            }
            return count;
        }

    }
}
