// using Elsa.Workflows.Runtime.Models;
//
// namespace Elsa.Workflows.Runtime.Services;
//
// public interface IWorkflowInstructionExecutor
// {
//     Task<IEnumerable<ExecuteWorkflowInstructionResult>> ExecuteInstructionAsync(IWorkflowInstruction instruction, CancellationToken cancellationToken = default);
//     Task<IEnumerable<ExecuteWorkflowInstructionResult>> ExecuteInstructionsAsync(IEnumerable<IWorkflowInstruction> instructions, CancellationToken cancellationToken = default);
//     Task<IEnumerable<DispatchWorkflowInstructionResult>> DispatchInstructionAsync(IWorkflowInstruction instruction, CancellationToken cancellationToken = default);
//     Task<IEnumerable<DispatchWorkflowInstructionResult>> DispatchInstructionsAsync(IEnumerable<IWorkflowInstruction> instructions, CancellationToken cancellationToken = default);
// }