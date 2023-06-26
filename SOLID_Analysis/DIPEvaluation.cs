using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public class DIPEvaluation
    {
        public int getAllDependentClasses { get; set; }
        public int countInheritors { get; set; }
        public int countAbstractAndInterfaceUsages { get; set; }
        public double VDIP { get; set; }
    }
}
