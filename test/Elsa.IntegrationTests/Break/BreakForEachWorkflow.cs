using System.Collections.Generic;
using Elsa.Activities;
using Elsa.Contracts;
using Elsa.Models;
using Elsa.Modules.Activities.Activities.Console;

namespace Elsa.IntegrationTests;

class BreakForEachWorkflow : IWorkflow
{
    public void Build(IWorkflowDefinitionBuilder workflow)
    {
        var items = new[] { "C#", "Rust", "Go" };
        var currentItem = new Variable<string>();

        workflow.WithRoot(new Sequence
        {
            Activities =
            {
                new WriteLine("Start"),
                new ForEach<string>
                {
                    Items = new Input<ICollection<string>>(items),
                    CurrentValue = currentItem,
                    Body = new Sequence
                    {
                        Activities =
                        {
                            new If(context => currentItem.Get(context) == "Rust")
                            {
                                Then = new Break()
                            },
                            new WriteLine(currentItem),
                            new WriteLine("Test")
                        }
                    }
                },
                new WriteLine("End"),
            }
        });
    }
}