// Copyright (c) Microsoft. All rights reserved.

namespace Microsoft.Agents.AI.DurableTask.Workflows;

/// <summary>
/// Represents the status of a pending external event when the workflow is waiting for human-in-the-loop input.
/// </summary>
/// <param name="EventName">The name of the event being waited for (the RequestPort ID).</param>
/// <param name="Input">The serialized input data that was passed to the RequestPort.</param>
internal sealed record PendingExternalEventStatus(
    string EventName,
    string Input);

/// <summary>
/// Represents the custom status written by the orchestration for streaming consumption.
/// </summary>
/// <remarks>
/// <para>
/// The Durable Task framework exposes <c>SerializedCustomStatus</c> on orchestration metadata,
/// which is the only orchestration state readable by external clients while the orchestration
/// is still running. The orchestrator writes this object via <c>SetCustomStatus</c> after each
/// superstep so that <see cref="DurableStreamingWorkflowRun"/> can poll for new events.
/// On orchestration completion the framework clears custom status, so events are also
/// embedded in the output via <see cref="DurableWorkflowResult"/>.
/// </para>
/// <para>
/// It also indicates when the workflow is waiting for external input (human-in-the-loop),
/// including the event name to raise and context about the request.
/// </para>
/// </remarks>
internal sealed class DurableWorkflowCustomStatus
{
    /// <summary>
    /// Gets or sets the pending external event status when waiting for human-in-the-loop input.
    /// </summary>
    public PendingExternalEventStatus? PendingEvent { get; set; }

    /// <summary>
    /// Gets or sets the serialized workflow events emitted so far.
    /// </summary>
    public List<string> Events { get; set; } = [];
}
