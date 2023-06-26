using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public class SRP
    {
        static double camcDef = 0.35;
        public static SRPEvaluation 
            SetParameters(INamedTypeSymbol c)
        {
            SRPEvaluation sRPEvaluation = 
                new SRPEvaluation();
            IMetricsCalculatorSRP metricsCalculator = 
                new MetricsCalculator();
            sRPEvaluation.classSize = metricsCalculator
                .GetNumberOfLines(c);
            sRPEvaluation.methodsCount = metricsCalculator
                .CountMethods(c);
            sRPEvaluation.responsibilitiesCount = metricsCalculator
                .CountResponsibilities(c);
            sRPEvaluation.CAMC = CAMC(c);
            return sRPEvaluation;
        }
        public static double VSRP(Project project)
        {
            ISearchCalsses searchCalsses = 
                new SearchCalsses();
            var cla = searchCalsses
                .AllClassAsync(project);
            var classes = searchCalsses.BaseClass(cla.Result, project);
            double csrp = 0;
            double ncsrp = 0;
            double vsrp = 0;
            foreach (var c in classes)
            {
                double camcMethods = CAMC(c);
                if(camcMethods > camcDef)
                {
                    csrp++;
                }
                else
                {
                    ncsrp--;
                }
            }
            vsrp = csrp / (csrp + ncsrp);
            return vsrp;
        }
        public static double CAMC(INamedTypeSymbol c)
        {
            double camc = 0;
            IMetricsCalculatorSRP metricsCalculator =
                new MetricsCalculator();
            double n = metricsCalculator.CountMethods(c);
            double t = metricsCalculator
                .CountParameterTypesClasses(c);
            ISearchMethods searchMethods = new SearchMethods();
            var methods = searchMethods.AllMethods(c);
            double p = 0;
            foreach (var method in methods)
            {
                p += metricsCalculator
                    .CountParameterTypesMethods(method);
            }
            camc = p / (t * n);
            return camc;
        }

    }
}
