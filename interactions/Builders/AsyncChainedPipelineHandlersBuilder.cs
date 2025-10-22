using Interactions.Core.Handlers;

namespace Interactions.Builders;

internal sealed class AsyncChainedPipelineHandlersBuilder<T1, T2, T3, T4, T5, T6>(
  AsyncPipelineHandlersBuilder<T1, T2, T3, T4> first,
  AsyncPipelineHandlersBuilder<T3, T4, T5, T6> second) : AsyncPipelineHandlersBuilder<T1, T2, T5, T6>(null) {

  public override AsyncHandler<T1, T2> End(AsyncHandler<T5, T6> handler) {
    return first.End(second.End(handler));
  }

}