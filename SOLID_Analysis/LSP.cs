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
    public class LSP
    {
        public static LSPEvaluation SetParameters
            (INamedTypeSymbol c, Project project)
        {
            LSPEvaluation lSPEvaluation = 
                new LSPEvaluation();
            ISearchCalsses searchCalsses = 
                new SearchCalsses();
            var classes = searchCalsses
                .AllClassAsync(project);
            var baseClasses = searchCalsses
                .BaseClass(classes.Result, project);
            IMetricsCalculatorLSP metricsCalculator = 
                new MetricsCalculator();
            lSPEvaluation.inheritanceDepth =
                metricsCalculator.GetInheritanceDepth(c);
            lSPEvaluation.numberOfDescendants =
                metricsCalculator.GetDependentClasses(c)
                .Count();
            lSPEvaluation.methodsCount =
                metricsCalculator.GetCalledMethods(c, baseClasses)
                .Count();
            return lSPEvaluation;
        }
        public static double VLSP
            (Solution solution, Project project)
        {
            ISearchCalsses searchCalsses = 
                new SearchCalsses();
            var cla = searchCalsses
                .AllClassAsync(project);
            var classes = searchCalsses.BaseClass(cla.Result, project);
            IMetricsCalculator metricsCalculator = 
                new MetricsCalculator();
            double vlsp = 0;
            foreach (var c in classes)
            {
                var derivedClasses = metricsCalculator
                    .GetAllDerivedClasses(c, solution);
                foreach(var d in derivedClasses)
                {
                    vlsp += CLSP(c, d);
                }
            }            
            return vlsp;
        }
        public static double CLSP
            (INamedTypeSymbol baseClass, 
            INamedTypeSymbol derivedClass)
        {
            IMetricsCalculator metricsCalculator =
                new MetricsCalculator();
            double n1 = metricsCalculator
                .CountMethods(baseClass);
            double n2 = metricsCalculator
                .CountMethods(derivedClass);
            double clsp = (n2 - n1) / (n2 + n1);
            return clsp;
        }
    }
}
