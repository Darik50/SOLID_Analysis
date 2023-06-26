using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public class ISPEvaluation
    {
        public int interfaceMethodsCount { get; set; }
        public double interfaceSeparationCoefficient { get; set; }
        public int implementingClassesCount { get; set; }
    }
}
