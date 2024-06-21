using Elsa.Mediator;
using Elsa.Mediator.Contracts;
using Elsa.Workflows.Runtime.Commands;
using Elsa.Workflows.Runtime.Contracts;
using Elsa.Workflows.Runtime.Requests;
using Elsa.Workflows.Runtime.Responses;

namespace Elsa.Workflows.Runtime.Services;

/// <summary>
/// Dispatches workflow cancellation requests to a local default worker.
/// </summary>
public class DefaultWorkflowCancellationDispatcher(ICommandSender commandSender) : IWorkflowCancellationDispatcher
{
    /// <inheritdoc />
    public async Task<DispatchCancelWorkflowsResponse> DispatchAsync(DispatchCancelWorkflowsRequest request, CancellationToken cancellationToken = default)
    {
        var command = new CancelWorkflowsCommand(request);
        await commandSender.SendAsync(command, CommandStrategy.Default, cancellationToken);
        return new DispatchCancelWorkflowsResponse();
    }
}