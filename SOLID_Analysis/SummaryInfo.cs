using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_Analysis
{
    public class SummaryInfo
    {        
        public ProjectMetrics CalculateOverallScore(List<PrincipleSection> principleSections)
        {
            double overallScore = 0;
            int SRPCount = 0;
            int OCPCount = 0;
            int LSPCount = 0;
            int ISPCount = 0;
            int DIPCount = 0;
            double SRPScore = 0;
            double OCPScore = 0;
            double LSPScore = 0;
            double ISPScore = 0;
            double DIPScore = 0;
            foreach (PrincipleSection section in principleSections)
            {
                double srpScore = 0;
                double ocpScore = 0;
                double lspScore = 0;
                double ispScore = 0;
                double dipScore = 0;
                if (section.SRPEvaluation != null)
                {
                    srpScore = CalculateSRPScore(section.SRPEvaluation.responsibilitiesCount, section.SRPEvaluation.classSize, section.SRPEvaluation.methodsCount, PrincipleWeights.WeightResponsibilities, PrincipleWeights.WeightClassSize, PrincipleWeights.WeightMethods);
                }
                else {
                    srpScore = 0;
                }
                if (section.OCPEvaluation != null)
                {
                    ocpScore = CalculateOCPScore(section.OCPEvaluation.inheritanceUsagePercent, section.OCPEvaluation.numberOfDescendants, section.OCPEvaluation.numberOfOverriddenMethods, section.OCPEvaluation.iheritanceDepth, PrincipleWeights.WeightUsage, PrincipleWeights.WeightSubclasses, PrincipleWeights.WeightOverrides, PrincipleWeights.WeightInheritance);
                }
                else
                {
                    ocpScore = 0;
                }
                if (section.LSPEvaluation != null)
                {
                    lspScore = CalculateLSPScore(section.LSPEvaluation.numberOfDescendants, section.LSPEvaluation.methodsCount, section.LSPEvaluation.inheritanceDepth, PrincipleWeights.WeightChildren, PrincipleWeights.WeightMethods, PrincipleWeights.WeightInheritance);
                }
                else
                {
                    lspScore = 0;
                }
                if (section.ISPEvaluation != null)
                {
                    ispScore = CalculateISPScore(section.ISPEvaluation.interfaceMethodsCount, section.ISPEvaluation.interfaceSeparationCoefficient, section.ISPEvaluation.implementingClassesCount, PrincipleWeights.WeightMethods, PrincipleWeights.WeightSeparation, PrincipleWeights.WeightImplementers);
                }
                else
                {
                    ispScore = 0;
                }
                if (section.DIPEvaluation != null)
                {
                    dipScore = CalculateDIPScore(section.DIPEvaluation.getAllDependentClasses, section.DIPEvaluation.countAbstractAndInterfaceUsages, section.DIPEvaluation.countInheritors, PrincipleWeights.WeightDependencies, PrincipleWeights.WeightUsage, PrincipleWeights.WeightInheritors);
                }
                else
                {
                    dipScore = 0;
                }
                if (srpScore != 0)
                {
                    SRPCount++;
                    SRPScore += srpScore;
                }
                if (ocpScore != 0)
                {
                    OCPCount++;
                    OCPScore += ocpScore;
                }
                if (lspScore != 0)
                {
                    LSPCount++;
                    LSPScore += lspScore;
                }
                if (ispScore != 0)
                {
                    ISPCount++;
                    ISPScore += ispScore;
                }
                if (dipScore != 0)
                {
                    DIPCount++;
                    DIPScore += dipScore;
                }
            }

            ProjectMetrics projectMetrics = new ProjectMetrics();
            projectMetrics.SRPScore = SRPScore / SRPCount;
            projectMetrics.OCPScore = OCPScore / OCPCount;
            projectMetrics.LSPScore = LSPScore / LSPCount;
            projectMetrics.ISPScore = ISPScore / ISPCount;
            projectMetrics.DIPScore = DIPScore / DIPCount;
            return projectMetrics;
        }
        public double CalculateSRPScore(int responsibilitiesCount, int classSize, int methodsCount, double weightResponsibilities, double weightClassSize, double weightMethods)
        {
            double score = (weightResponsibilities * responsibilitiesCount + weightClassSize * classSize + weightMethods * methodsCount) / (weightResponsibilities + weightClassSize + weightMethods);
            return score;
        }

        public double CalculateOCPScore(double usagePercentage, int subclassesCount, int overridesCount, int inheritanceDepth, double weightUsage, double weightSubclasses, double weightOverrides, double weightInheritance)
        {
            double score = (weightUsage * usagePercentage + weightSubclasses * subclassesCount + weightOverrides * overridesCount + weightInheritance * inheritanceDepth) / (weightUsage + weightSubclasses + weightOverrides + weightInheritance);
            return score;
        }
        public double CalculateLSPScore(int childrenCount, int invocableMethodsCount, int inheritanceDepth, double weightChildren, double weightMethods, double weightInheritance)
        {
            double score = (weightChildren * childrenCount + weightMethods * invocableMethodsCount + weightInheritance * inheritanceDepth) / (weightChildren + weightMethods + weightInheritance);
            return score;
        }
        public double CalculateISPScore(int methodsCount, double interfaceSeparationCoefficient, int implementingClassesCount, double weightMethods, double weightSeparation, double weightImplementers)
        {
            double score = (weightMethods * methodsCount + weightSeparation * interfaceSeparationCoefficient + weightImplementers * implementingClassesCount) / (weightMethods + weightSeparation + weightImplementers);
            return score;
        }
        public double CalculateDIPScore(int dependenciesCount, int interfaceUsageCount, int inheritorsCount, double weightDependencies, double weightUsage, double weightInheritors)
        {
            double score = (weightDependencies * dependenciesCount + weightUsage * interfaceUsageCount + weightInheritors * inheritorsCount) / (weightDependencies + weightUsage + weightInheritors);
            return score;
        }

    }
}


