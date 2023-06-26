using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public class LSPEvaluation
    {
        public int inheritanceDepth { get; set; }
        public int numberOfDescendants { get; set; }
        public int methodsCount { get; set; }
        public int CLSP { get; set; }
    }
}
