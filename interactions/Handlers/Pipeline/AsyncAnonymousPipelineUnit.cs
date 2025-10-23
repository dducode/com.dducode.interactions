using Interactions.Actions;

namespace Interactions.Handlers.Pipeline;

internal sealed class AsyncAnonymousPipelineUnit<T1, T2, T3, T4>(AsyncPipeline<T1, T2, T3, T4> pipeline) : AsyncPipelineUnit<T1, T2, T3, T4> {

  protected internal override ValueTask<T2> Invoke(T1 input, Func<T3, CancellationToken, ValueTask<T4>> next, CancellationToken token = default) {
    return pipeline(input, next, token);
  }

}