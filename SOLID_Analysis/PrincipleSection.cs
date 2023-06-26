using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public class PrincipleSection
    {
        public string name { get; set; }
        public SRPEvaluation SRPEvaluation { get; set; }
        public OCPEvaluation OCPEvaluation { get; set; }
        public LSPEvaluation LSPEvaluation { get; set; }
        public ISPEvaluation ISPEvaluation { get; set; }
        public DIPEvaluation DIPEvaluation { get; set; }
      
    }
}
