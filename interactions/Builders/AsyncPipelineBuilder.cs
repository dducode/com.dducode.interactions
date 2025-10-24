using System.Diagnostics.Contracts;
using Interactions.Core;
using Interactions.Pipelines;

namespace Interactions.Builders;

public class AsyncPipelineBuilder<T1, T2, T3, T4> {

  private readonly AsyncPipeline<T1, T2, T3, T4> _pipeline;

  internal AsyncPipelineBuilder(AsyncPipeline<T1, T2, T3, T4> pipeline) {
    _pipeline = pipeline;
  }

  public AsyncPipelineBuilder<T1, T2, T5, T6> Use<T5, T6>(AsyncPipeline<T3, T4, T5, T6> pipeline) {
    return new AsyncRecursivePipelineBuilder<T1, T2, T3, T4, T5, T6>(this, new AsyncPipelineBuilder<T3, T4, T5, T6>(pipeline));
  }

  [Pure]
  public virtual AsyncHandler<T1, T2> End(AsyncHandler<T3, T4> handler) {
    return new AsyncPipelineHandler<T1, T2, T3, T4>(_pipeline, handler);
  }

}