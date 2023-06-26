using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public class Input
    {
        public static Solution read_Solution(string solutionPath)
        {
            using (var workspace = MSBuildWorkspace.Create())
            {
                Solution currSolution = workspace.OpenSolutionAsync(solutionPath).Result;
                return currSolution;
            }
        }
        public static Project[] read_Projects(Solution solution)
        {
            using (var workspace = MSBuildWorkspace.Create())
            {
                return solution.Projects.ToArray();
            }
        }
        public static Document[] read_Documents(Project project)
        {
            using (var workspace = MSBuildWorkspace.Create())
            {
                return project.Documents.ToArray();
            }
        }
    }
}
