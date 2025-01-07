using Elsa.Common.Models;
using Elsa.Workflows.ComponentTests.Abstractions;
using Elsa.Workflows.ComponentTests.Fixtures;
using Elsa.Workflows.ComponentTests.Scenarios.WorkflowInstanceCreation.Workflows;
using Elsa.Workflows.Management;
using Elsa.Workflows.Models;
using Elsa.Workflows.Runtime;
using Elsa.Workflows.Runtime.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Elsa.Workflows.ComponentTests.Scenarios.WorkflowInstanceCreation;

public class CreateWorkflowInstanceTests : AppComponentTest
{
    private readonly IWorkflowRuntime _workflowRuntime;
    private readonly IWorkflowInstanceManager _workflowInstanceManager;
    
    public CreateWorkflowInstanceTests(App app) : base(app)
    {
        _workflowRuntime = Scope.ServiceProvider.GetRequiredService<IWorkflowRuntime>();
        _workflowInstanceManager = Scope.ServiceProvider.GetRequiredService<IWorkflowInstanceManager>();
    }
    
    [Fact]
    public async Task CreateWorkflowInstance_WithSetNameConfigured_ShouldOverrideDefinitionName()
    {
        var workflowClient = await _workflowRuntime.CreateClientAsync();
        await workflowClient.CreateAndRunInstanceAsync(new CreateAndRunWorkflowInstanceRequest
        {
            WorkflowDefinitionHandle = WorkflowDefinitionHandle.ByDefinitionId(SetNameWorkflow.DefinitionId, VersionOptions.Published)
        });
        
        var result = await workflowClient.RunInstanceAsync(RunWorkflowInstanceRequest.Empty);

        var instance = await _workflowInstanceManager.FindByIdAsync(result.WorkflowInstanceId);
       
        Assert.Equal(WorkflowStatus.Finished, result.Status);
        Assert.Equal(WorkflowSubStatus.Finished, result.SubStatus);
        Assert.Equal("Completed-Test", instance?.Name);
    }
    
    
    [Fact]
    public async Task CreateWorkflowInstance_WithNoSetNameConfigured_ShouldHaveDefinitionName()
    {
        var workflowClient = await _workflowRuntime.CreateClientAsync();
        await workflowClient.CreateAndRunInstanceAsync(new CreateAndRunWorkflowInstanceRequest
        {
            WorkflowDefinitionHandle = WorkflowDefinitionHandle.ByDefinitionId(UseWorkflowNameWorkflow.DefinitionId, VersionOptions.Published)
        });
        
        var result = await workflowClient.RunInstanceAsync(RunWorkflowInstanceRequest.Empty);

        var instance = await _workflowInstanceManager.FindByIdAsync(result.WorkflowInstanceId);
       
        Assert.Equal(WorkflowStatus.Finished, result.Status);
        Assert.Equal(WorkflowSubStatus.Finished, result.SubStatus);
        Assert.Equal("Test-Workflow", instance?.Name);
    }
}