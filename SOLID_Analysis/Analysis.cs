using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FindSymbols;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public class Analysis
    {
        public static ReportData AnalyzeDecision
            (Project project, Solution solution)
        {
            ReportData reportData = new ReportData();
            ISearchCalsses searchCalsses = 
                new SearchCalsses();
            var cs = searchCalsses.AllClassAsync(project);
            var classes = searchCalsses
                .BaseClass(cs.Result, project);
            reportData.principleSections = 
                new List<PrincipleSection>();
            foreach (var c in classes)
            {
                SRPEvaluation sRPEvaluation = 
                    SRP.SetParameters(c);
                OCPEvaluation oCPEvaluation = 
                    OCP.SetParameters(c);
                LSPEvaluation lSPEvaluation = 
                    LSP.SetParameters(c, project);
                ISPEvaluation iSPEvaluation = 
                    ISP.SetParameters(c, project);
                DIPEvaluation dIPEvaluation = 
                    DIP.SetParameters(c, project);
                PrincipleSection principleSection = 
                    new PrincipleSection();
                principleSection.name = c.Name;
                principleSection.SRPEvaluation = sRPEvaluation;
                principleSection.OCPEvaluation = oCPEvaluation;
                principleSection.LSPEvaluation = lSPEvaluation;
                principleSection.ISPEvaluation = iSPEvaluation;
                principleSection.DIPEvaluation = dIPEvaluation;
                reportData.principleSections.Add(principleSection);
            }
            var interfaces = searchCalsses
                .GetAllInterfaces(project);
            foreach (var i in interfaces)
            {
                ISPEvaluation iSPEvaluation =
                    ISP.SetParameters(i, project);
                PrincipleSection principleSection =
                    new PrincipleSection();
                principleSection.name = i.Name;
                principleSection.ISPEvaluation = iSPEvaluation;
                reportData.principleSections.Add(principleSection);
            }
            ProjectMetrics projectMetrics = new ProjectMetrics();            
            SummaryInfo summaryInfo = new SummaryInfo();
            projectMetrics = summaryInfo.CalculateOverallScore(reportData.principleSections);
            projectMetrics.VSRPScore = SRP.VSRP(project);
            projectMetrics.VOCPScore = OCP.VOCP(project);
            projectMetrics.VLSPScore = LSP.VLSP(solution, project);
            projectMetrics.VISPScore = ISP.VISP(solution, project);
            projectMetrics.VDIPScore = DIP.VDIP(project);
            reportData.summaryInfo = projectMetrics;
            return reportData;
        }
    }
}
