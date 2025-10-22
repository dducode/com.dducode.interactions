using Interactions.Actions;
using Interactions.Core.Handlers;
using Interactions.Handlers;

namespace Interactions.Builders;

public class AsyncPipelineHandlersBuilder<T1, T2, T3, T4> {

  private readonly AsyncPipeline<T1, T2, T3, T4> _delegate;

  internal AsyncPipelineHandlersBuilder(AsyncPipeline<T1, T2, T3, T4> func) {
    _delegate = func;
  }

  public AsyncPipelineHandlersBuilder<T1, T2, T5, T6> Use<T5, T6>(AsyncPipeline<T3, T4, T5, T6> func) {
    return new AsyncChainedPipelineHandlersBuilder<T1, T2, T3, T4, T5, T6>(this, new AsyncPipelineHandlersBuilder<T3, T4, T5, T6>(func));
  }

  public AsyncPipelineHandlersBuilder<T1, T2, T, T> Use<T>(AsyncPipeline<T3, T4, T, T> func) {
    return new AsyncChainedPipelineHandlersBuilder<T1, T2, T3, T4, T, T>(this, new AsyncPipelineHandlersBuilder<T3, T4, T, T>(func));
  }

  public AsyncPipelineHandlersBuilder<T1, T2, T3, T4> Use(AsyncPipeline<T3, T4, T3, T4> func) {
    return new AsyncChainedPipelineHandlersBuilder<T1, T2, T3, T4, T3, T4>(this, new AsyncPipelineHandlersBuilder<T3, T4, T3, T4>(func));
  }

  public virtual AsyncHandler<T1, T2> End(AsyncHandler<T3, T4> handler) {
    return new AsyncPipelineHandler<T1, T2, T3, T4>(_delegate, handler);
  }

}