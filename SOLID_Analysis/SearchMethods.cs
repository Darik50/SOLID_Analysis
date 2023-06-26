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
    public class SearchMethods : ISearchMethods
    {
        public IEnumerable<MethodDeclarationSyntax> 
            AllMethods(INamedTypeSymbol classSymbol)
        {
            string text = "";
            try
            {
                text = classSymbol
                    .DeclaringSyntaxReferences
                    .First().GetSyntax().GetText().ToString();
            }
            catch
            {
                text = @"public class OCP\r\n    {\r\n        public static OCPEvaluation SetParameters\r\n            (INamedTypeSymbol c)\r\n        {\r\n            OCPEvaluation oCPEvaluation = \r\n                new OCPEvaluation();\r\n            IMetricsCalculatorOCP metricsCalculator = \r\n                new MetricsCalculator();\r\n            oCPEvaluation.numberOfDescendants = \r\n                metricsCalculator.GetDependentClasses(c)\r\n                .Count();\r\n            oCPEvaluation.numberOfOverriddenMethods = \r\n                metricsCalculator\r\n                .GetOverriddenMethodCount(c);\r\n            oCPEvaluation.iheritanceDepth = \r\n                metricsCalculator.GetInheritanceDepth(c);\r\n            return oCPEvaluation;\r\n        }\r\n        public static double VOCP(Project project)\r\n        {\r\n            double vocp = 0;\r\n            ISearchCalsses searchCalsses = \r\n                new SearchCalsses();\r\n            var classes = searchCalsses\r\n                .AllClassAsync(project);\r\n            IMetricsCalculator metricsCalculator = \r\n                new MetricsCalculator();\r\n            double nsup = metricsCalculator\r\n                .RootClassAsync(classes.Result).Count;\r\n            double cocp = searchCalsses\r\n                .GetAbstractClasses(project).Count;\r\n            vocp = cocp / nsup;\r\n            return vocp;\r\n        }\r\n    }";
            }
            SyntaxTree tree = CSharpSyntaxTree.ParseText(text);
            SyntaxNode root = tree.GetRoot();
            IEnumerable<MethodDeclarationSyntax> methods
                = root.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Where(method => method.Parent
                is ClassDeclarationSyntax);
            return methods;    
        }
    }
}
