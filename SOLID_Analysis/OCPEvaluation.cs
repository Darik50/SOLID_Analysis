using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public class OCPEvaluation
    {
        public int numberOfDescendants { get; set; }
        public int numberOfOverriddenMethods { get; set; }
        public int inheritanceUsagePercent { get; set; }
        public int iheritanceDepth{ get; set; }
    }
}
