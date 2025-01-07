﻿using Elsa.Workflows.Activities;
using Elsa.Workflows.Models;

namespace Elsa.Workflows.ComponentTests.Scenarios.WorkflowInstanceCreation.Workflows;

public class UseWorkflowNameWorkflow : WorkflowBase
{
    public static readonly string DefinitionId = Guid.NewGuid().ToString();

    protected override void Build(IWorkflowBuilder builder)
    {
        builder.WithDefinitionId(DefinitionId);
        builder.Name = "Test-Workflow";
        builder.Root = new Sequence
        {
            Activities =
            {
              new Start(),
              new End()
            }
        };
    }
}