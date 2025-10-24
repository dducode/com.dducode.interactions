using System.Diagnostics.Contracts;
using Interactions.Core;
using Interactions.Pipelines;

namespace Interactions.Builders;

public class PipelineBuilder<T1, T2, T3, T4> {

  private readonly Pipeline<T1, T2, T3, T4> _pipeline;

  internal PipelineBuilder(Pipeline<T1, T2, T3, T4> pipeline) {
    _pipeline = pipeline;
  }

  public PipelineBuilder<T1, T2, T5, T6> Use<T5, T6>(Pipeline<T3, T4, T5, T6> pipeline) {
    return new RecursivePipelineBuilder<T1, T2, T3, T4, T5, T6>(this, new PipelineBuilder<T3, T4, T5, T6>(pipeline));
  }

  [Pure]
  public virtual Handler<T1, T2> End(Handler<T3, T4> handler) {
    return new PipelineHandler<T1, T2, T3, T4>(_pipeline, handler);
  }

}