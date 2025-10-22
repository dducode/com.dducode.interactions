using Interactions.Actions;
using Interactions.Core.Handlers;
using Interactions.Handlers;

namespace Interactions.Builders;

public class PipelineHandlersBuilder<T1, T2, T3, T4> {

  private readonly Pipeline<T1, T2, T3, T4> _delegate;

  internal PipelineHandlersBuilder(Pipeline<T1, T2, T3, T4> func) {
    _delegate = func;
  }

  public PipelineHandlersBuilder<T1, T2, T5, T6> Use<T5, T6>(Pipeline<T3, T4, T5, T6> func) {
    return new ChainedPipelineHandlersBuilder<T1, T2, T3, T4, T5, T6>(this, new PipelineHandlersBuilder<T3, T4, T5, T6>(func));
  }

  public PipelineHandlersBuilder<T1, T2, T, T> Use<T>(Pipeline<T3, T4, T, T> func) {
    return new ChainedPipelineHandlersBuilder<T1, T2, T3, T4, T, T>(this, new PipelineHandlersBuilder<T3, T4, T, T>(func));
  }

  public PipelineHandlersBuilder<T1, T2, T3, T4> Use(Pipeline<T3, T4, T3, T4> func) {
    return new ChainedPipelineHandlersBuilder<T1, T2, T3, T4, T3, T4>(this, new PipelineHandlersBuilder<T3, T4, T3, T4>(func));
  }

  public virtual Handler<T1, T2> End(Handler<T3, T4> handler) {
    return new PipelineHandler<T1, T2, T3, T4>(_delegate, handler);
  }

}