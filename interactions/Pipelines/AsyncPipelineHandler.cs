using Interactions.Core;

namespace Interactions.Pipelines;

internal sealed class AsyncPipelineHandler<T1, T2, T3, T4>(AsyncPipeline<T1, T2, T3, T4> pipeline, AsyncHandler<T3, T4> next) : AsyncHandler<T1, T2> {

  protected internal override ValueTask<T2> Handle(T1 input, CancellationToken token = default) {
    return pipeline.Invoke(input, next.Handle, token);
  }

}