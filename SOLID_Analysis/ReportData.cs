using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public class ReportData
    {
        public ProjectMetrics summaryInfo { get; set; }
        public List<PrincipleSection> principleSections 
        { get; set; }
    }
}
