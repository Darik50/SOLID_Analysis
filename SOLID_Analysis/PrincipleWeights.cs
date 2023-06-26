using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public class PrincipleWeights
    {
        public static double WeightResponsibilities = 0.07;
        public static double WeightClassSize = 0.007;
        public static double WeightMethods = 0.07;

        public static double WeightUsage = 2;
        public static double WeightSubclasses = 0.14;
        public static double WeightOverrides = 0.14;
        public static double WeightInheritance = 0.2;

        public static double WeightChildren = 0.14;
        public static double WeightMethodsLSP = 0.05;
        public static double WeightInheritanceLSP = 0.14;

        public static double WeightMethodsISP = 0.07;
        public static double WeightSeparation = 2;
        public static double WeightImplementers = 0.1;

        public static double WeightDependencies = 0.07;
        public static double WeightUsageDIP = 2;
        public static double WeightInheritors = 0.07;
    }
}
