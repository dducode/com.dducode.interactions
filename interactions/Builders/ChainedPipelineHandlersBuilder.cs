using Interactions.Core.Handlers;

namespace Interactions.Builders;

internal sealed class ChainedPipelineHandlersBuilder<T1, T2, T3, T4, T5, T6>(
  PipelineHandlersBuilder<T1, T2, T3, T4> first,
  PipelineHandlersBuilder<T3, T4, T5, T6> second) : PipelineHandlersBuilder<T1, T2, T5, T6>(null) {

  public override Handler<T1, T2> End(Handler<T5, T6> handler) {
    return first.End(second.End(handler));
  }

}