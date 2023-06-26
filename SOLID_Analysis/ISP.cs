using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public class ISP
    {
        public static ISPEvaluation SetParameters
            (INamedTypeSymbol c, Project project)
        {
            ISPEvaluation iSPEvaluation = 
                new ISPEvaluation();
            ISearchCalsses searchCalsses = 
                new SearchCalsses();
            IMetricsCalculatorISP metricsCalculator =
                new MetricsCalculator();
            iSPEvaluation.interfaceMethodsCount = 
                metricsCalculator
                .GetInterfaceMethodsCount(c);
            iSPEvaluation.interfaceSeparationCoefficient =
                metricsCalculator.CalculateISP(c);
            iSPEvaluation.implementingClassesCount = 
                metricsCalculator
                .CountClassesImplementingInterface
                (c, searchCalsses.AllClassAsync(project).Result);
            return iSPEvaluation;
        }
        public static double VISP(Solution solution, Project project)
        {
            ISearchCalsses searchCalsses = new SearchCalsses();
            IMetricsCalculator metricsCalculator = 
                new MetricsCalculator();
            var interfaces = searchCalsses
                .GetAllInterfaces(project);
            double visp = 0;
            double cisp = 0;
            double ncisp = 0;
            bool flag = true;
            foreach(var i in interfaces)
            {
                if(SRP.CAMC(i) <= 1)
                {
                    var derivedClasses = metricsCalculator
                        .GetAllDerivedClasses(i, solution);
                    foreach(var d in derivedClasses)
                    {
                        if(LSP.CLSP(i,d) < 0.5)
                        {
                            flag = false;
                        }
                    }
                }
                else
                {
                    flag = false;
                }
                if (flag)
                {
                    cisp++;
                }
                else
                {
                    ncisp++;
                }
            }
            visp = cisp / (ncisp + cisp);
            return visp;
        }
    }
}
