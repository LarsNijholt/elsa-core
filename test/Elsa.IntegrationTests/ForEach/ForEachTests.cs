using System.Linq;
using System.Threading.Tasks;
using Elsa.Builders;
using Elsa.Contracts;
using Elsa.Testing.Shared;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace Elsa.IntegrationTests;

public class ForEachTests
{
    private readonly IWorkflowRunner _workflowRunner;
    private readonly CapturingTextWriter _capturingTextWriter = new();

    public ForEachTests(ITestOutputHelper testOutputHelper)
    {
        var services = new TestApplicationBuilder(testOutputHelper).WithCapturingTextWriter(_capturingTextWriter).Build();
        _workflowRunner = services.GetRequiredService<IWorkflowRunner>();
    }

    [Fact(DisplayName = "ForEach outputs each iteration")]
    public async Task Test1()
    {
        var items = new[] { "C#", "Rust", "Go"};
        var workflow = new WorkflowDefinitionBuilder().BuildWorkflow(new ForEachWorkflow(items));
        await _workflowRunner.RunAsync(workflow);
        var lines = _capturingTextWriter.Lines.ToList();
        Assert.Equal(items, lines);
    }
}