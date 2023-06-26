using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ReportData reportData = new ReportData();
            MSBuildLocator.RegisterDefaults();
            MSBuildWorkspace mSBuildWorkspace = MSBuildWorkspace.Create();
            var workspace = mSBuildWorkspace;
            string solutionPath = @"path";            
            var solution = await workspace.OpenSolutionAsync(solutionPath);
            var projects = solution.Projects.ToList();
            for (int i =0; i < projects.Count;i++) 
            {
                reportData = Analysis.AnalyzeDecision(projects[i], solution);
                IMetricsCalculator metricsCalculator = new MetricsCalculator();
                ISearchCalsses searchCalsses = new SearchCalsses();
                int countInterfaces = searchCalsses.GetAllInterfaces(projects[i]).Count;
                var classes = searchCalsses.AllClassAsync(projects[i]);
                var baseClasses = searchCalsses.BaseClass(classes.Result, projects[i]);
                int countClasses = baseClasses.Count();
                int countMethods = 0;
                foreach (var j in baseClasses)
                {
                    countMethods += metricsCalculator.CountMethods(j);
                }
            }
            Console.ReadLine();
        }        
    }
}
