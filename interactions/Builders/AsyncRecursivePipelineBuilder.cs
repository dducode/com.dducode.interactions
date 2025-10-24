using Interactions.Core;

namespace Interactions.Builders;

internal sealed class AsyncRecursivePipelineBuilder<T1, T2, T3, T4, T5, T6>(
  AsyncPipelineBuilder<T1, T2, T3, T4> first,
  AsyncPipelineBuilder<T3, T4, T5, T6> second) : AsyncPipelineBuilder<T1, T2, T5, T6>(null) {

  public override AsyncHandler<T1, T2> End(AsyncHandler<T5, T6> handler) {
    return first.End(second.End(handler));
  }

}