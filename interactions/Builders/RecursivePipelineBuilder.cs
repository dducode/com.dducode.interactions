using Interactions.Core;

namespace Interactions.Builders;

internal sealed class RecursivePipelineBuilder<T1, T2, T3, T4, T5, T6>(
  PipelineBuilder<T1, T2, T3, T4> first,
  PipelineBuilder<T3, T4, T5, T6> second) : PipelineBuilder<T1, T2, T5, T6>(null) {

  public override Handler<T1, T2> End(Handler<T5, T6> handler) {
    return first.End(second.End(handler));
  }

}