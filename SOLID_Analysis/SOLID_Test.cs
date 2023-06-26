using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using NUnit.Framework;
using SOLID_Analysis;
using System.Linq;

[TestFixture]
public class SOLID_Test
{
    [Test]
    public void Test_InhClass_1()
    {
        string path = @"Path\Inh.sln";
        MSBuildLocator.RegisterDefaults();
        Solution solution = Input.read_Solution(path);
        Project[] projects = Input.read_Projects(solution);
        ISearchCalsses searchCalsses = new SearchCalsses();
        int result = searchCalsses.InhClassAsync
            (searchCalsses.AllClassAsync(projects[0]).Result)
            .Count;
        Assert.AreEqual(4, result);
    }
    [Test]
    public void Test_BaseClass_1()
    {
        string path = @"Path\Test2.sln";
        Solution solution = Input.read_Solution(path);
        Project[] projects = Input.read_Projects(solution);
        ISearchCalsses searchCalsses = new SearchCalsses();
        int result = searchCalsses.BaseClass
            (searchCalsses.AllClassAsync(projects[0])
            .Result, projects[0]).Count;
        Assert.AreEqual(3, result);
    }
    [Test]
    public void Test_Methods_1()
    {
        string path = @"Path\Test2.sln";
        Solution solution = Input.read_Solution(path);
        Project[] projects = Input.read_Projects(solution);
        ISearchCalsses searchCalsses = new SearchCalsses();
        IMetricsCalculator metricsCalculator = 
            new MetricsCalculator();
        var baseClasses = searchCalsses.BaseClass
            (searchCalsses.AllClassAsync(projects[0])
            .Result, projects[0]);
        int result = 0;
        foreach (var i in baseClasses)
        {
            result += metricsCalculator.CountMethods(i);
        }        
        Assert.AreEqual(5, result);
    }
    [Test]
    public void Test_Interfaces_1()
    {
        string path = @"Path\ISP.sln";
        Solution solution = Input.read_Solution(path);
        Project[] projects = Input.read_Projects(solution);
        ISearchCalsses searchCalsses = new SearchCalsses();
        int result = searchCalsses
            .GetAllInterfaces(projects[0]).Count;
        Assert.AreEqual(2, result);
    }
    [Test]
    public void Test_Abstract_1()
    {
        string path = @"Path\Abstract.sln";
        Solution solution = Input.read_Solution(path);
        Project[] projects = Input.read_Projects(solution);
        ISearchCalsses searchCalsses = new SearchCalsses();
        int result = searchCalsses.
            GetAbstractClasses(projects[0]).Count;
        Assert.AreEqual(40, result);
    }
    public void Test_RootClass_1()
    {
        string path = @"Path\Inh.sln";
        Solution solution = Input.read_Solution(path);
        Project[] projects = Input.read_Projects(solution);
        ISearchCalsses searchCalsses = new SearchCalsses();
        IMetricsCalculator metricsCalculator = 
            new MetricsCalculator();
        var result = metricsCalculator.RootClassAsync
            (searchCalsses.AllClassAsync(projects[0]).Result);
        Assert.AreEqual(1, result);
    }
    [Test]
    public void Test_BaseClass_2()
    {
        string path = @"Path\SRP.sln";
        Solution solution = Input.read_Solution(path);
        Project[] projects = Input.read_Projects(solution);
        ISearchCalsses searchCalsses = new SearchCalsses();
        int result = searchCalsses.BaseClass
            (searchCalsses.AllClassAsync(projects[0])
            .Result, projects[0]).Count;
        Assert.AreEqual(2, result);
    }
    [Test]
    public void Test_BaseClass_3()
    {
        string path = @"Path\ISP.sln";
        Solution solution = Input.read_Solution(path);
        Project[] projects = Input.read_Projects(solution);
        ISearchCalsses searchCalsses = new SearchCalsses();
        int result = searchCalsses.BaseClass(searchCalsses
            .AllClassAsync(projects[0]).Result, projects[0])
            .Count;
        Assert.AreEqual(2, result);
    }
    [Test]
    public void Test_Methods_2()
    {
        string path = @"Path\SRP.sln";
        Solution solution = Input.read_Solution(path);
        Project[] projects = Input.read_Projects(solution);
        ISearchCalsses searchCalsses = new SearchCalsses();
        IMetricsCalculator metricsCalculator = 
            new MetricsCalculator();
        var baseClasses = searchCalsses
            .BaseClass(searchCalsses
            .AllClassAsync(projects[0]).Result, projects[0]);
        int result = 0;
        foreach (var i in baseClasses)
        {
            result += metricsCalculator.CountMethods(i);
        }
        Assert.AreEqual(7, result);
    }
    [Test]
    public void Test_Methods_3()
    {
        string path = @"Path\ISP.sln";
        Solution solution = Input.read_Solution(path);
        Project[] projects = Input.read_Projects(solution);
        ISearchCalsses searchCalsses = new SearchCalsses();
        IMetricsCalculator metricsCalculator = 
            new MetricsCalculator();
        var baseClasses = searchCalsses.BaseClass
            (searchCalsses.AllClassAsync(projects[0]).Result,
            projects[0]);
        int result = 0;
        foreach (var i in baseClasses)
        {
            result += metricsCalculator.CountMethods(i);
        }
        Assert.AreEqual(4, result);
    }
    [Test]
    public void Test_Methods_4()
    {
        string path = @"Path\Inh.sln";
        Solution solution = Input.read_Solution(path);
        Project[] projects = Input.read_Projects(solution);
        ISearchCalsses searchCalsses = new SearchCalsses();
        IMetricsCalculator metricsCalculator = 
            new MetricsCalculator();
        var baseClasses = searchCalsses.BaseClass(searchCalsses
            .AllClassAsync(projects[0]).Result, projects[0]);
        int result = 0;
        foreach (var i in baseClasses)
        {
            result += metricsCalculator.CountMethods(i);
        }
        Assert.AreEqual(2, result);
    }
    [Test]
    public void Test_Methods_5()
    {
        string path = @"Path\Abstract.sln";
        Solution solution = Input.read_Solution(path);
        Project[] projects = Input.read_Projects(solution);
        ISearchCalsses searchCalsses = new SearchCalsses();
        IMetricsCalculator metricsCalculator = 
            new MetricsCalculator();
        var baseClasses = searchCalsses.BaseClass(searchCalsses
            .AllClassAsync(projects[0]).Result, projects[0]);
        int result = 0;
        foreach (var i in baseClasses)
        {
            result += metricsCalculator.CountMethods(i);
        }
        Assert.AreEqual(5, result);
    }
    [Test]
    public void Test_BaseClass_4()
    {
        string path = @"Path\Abstract.sln";
        Solution solution = Input.read_Solution(path);
        Project[] projects = Input.read_Projects(solution);
        ISearchCalsses searchCalsses = new SearchCalsses();
        int result = searchCalsses.BaseClass(searchCalsses
            .AllClassAsync(projects[0]).Result, projects[0])
            .Count;
        Assert.AreEqual(5, result);
    }
    [Test]
    public void Test_BaseClass_5()
    {
        string path = @"Path\Inh.sln";
        Solution solution = Input.read_Solution(path);
        Project[] projects = Input.read_Projects(solution);
        ISearchCalsses searchCalsses = new SearchCalsses();
        int result = searchCalsses.BaseClass
            (searchCalsses.AllClassAsync(projects[0]).Result, 
            projects[0]).Count;
        Assert.AreEqual(2, result);
    }
}