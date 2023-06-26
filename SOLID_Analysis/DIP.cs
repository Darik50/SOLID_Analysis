using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public class DIP
    {
        public static DIPEvaluation SetParameters
            (INamedTypeSymbol c, Project project)
        {
            DIPEvaluation dIPEvaluation = 
                new DIPEvaluation();
            ISearchCalsses searchCalsses = 
                new SearchCalsses();
            IMetricsCalculatorDIP metricsCalculator =
                new MetricsCalculator();       
            dIPEvaluation.countAbstractAndInterfaceUsages =
                metricsCalculator
                .CountAbstractAndInterfaceUsages(c);
            dIPEvaluation.countInheritors = metricsCalculator
                .CountInheritors(c, project);
            dIPEvaluation.getAllDependentClasses = 
                metricsCalculator
                .GetAllDependentClasses(c).Count();
            return dIPEvaluation;
        }
        public static double VDIP(Project project)
        {
            ISearchCalsses searchCalsses = new SearchCalsses();
            var cla = searchCalsses.AllClassAsync(project);
            var classes = searchCalsses.BaseClass(cla.Result, project);
            double vdip = 0;
            double cdip = 0;
            double ndep = 0;
            IMetricsCalculator metricsCalculator =
                new MetricsCalculator();
            foreach (var c in classes)
            {
                if(metricsCalculator
                    .GetAllDependentClasses(c).Count > 0)
                {
                    ndep++;
                }
            }
            cdip = searchCalsses.GetAbstractClasses(project).Count;
            vdip = cdip / ndep;
            DIPEvaluation dIPEvaluation = new DIPEvaluation();
            dIPEvaluation.VDIP = vdip;
            return dIPEvaluation.VDIP;
        }
    }
}
