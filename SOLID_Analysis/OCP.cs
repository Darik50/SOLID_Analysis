using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public class OCP
    {
        public static OCPEvaluation SetParameters
            (INamedTypeSymbol c)
        {
            OCPEvaluation oCPEvaluation = 
                new OCPEvaluation();
            IMetricsCalculatorOCP metricsCalculator = 
                new MetricsCalculator();
            oCPEvaluation.numberOfDescendants = 
                metricsCalculator.GetDependentClasses(c)
                .Count();
            oCPEvaluation.numberOfOverriddenMethods = 
                metricsCalculator
                .GetOverriddenMethodCount(c);
            oCPEvaluation.iheritanceDepth = 
                metricsCalculator.GetInheritanceDepth(c);
            return oCPEvaluation;
        }
        public static double VOCP(Project project)
        {
            double vocp = 0;
            ISearchCalsses searchCalsses = 
                new SearchCalsses();
            var cla = searchCalsses
                .AllClassAsync(project);
            var classes = searchCalsses.BaseClass(cla.Result, project);
            IMetricsCalculator metricsCalculator = 
                new MetricsCalculator();
            double nsup = metricsCalculator
                .RootClassAsync(classes).Count;
            double cocp = searchCalsses
                .GetAbstractClasses(project).Count;
            vocp = cocp / nsup;
            return vocp;
        }
    }
}
