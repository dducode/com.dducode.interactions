using Interactions.Actions;
using Interactions.Core;
using Interactions.Handlers.Pipeline;

namespace Interactions.Builders;

public class AsyncPipelineHandlersBuilder<T1, T2, T3, T4> {

  private readonly AsyncPipelineUnit<T1, T2, T3, T4> _unit;

  internal AsyncPipelineHandlersBuilder(AsyncPipelineUnit<T1, T2, T3, T4> func) {
    _unit = func;
  }

  public AsyncPipelineHandlersBuilder<T1, T2, T5, T6> Use<T5, T6>(AsyncPipelineUnit<T3, T4, T5, T6> func) {
    return new AsyncRecursivePipelineHandlersBuilder<T1, T2, T3, T4, T5, T6>(this, new AsyncPipelineHandlersBuilder<T3, T4, T5, T6>(func));
  }

  public AsyncPipelineHandlersBuilder<T1, T2, T, T> Use<T>(AsyncPipelineUnit<T3, T4, T, T> func) {
    return new AsyncRecursivePipelineHandlersBuilder<T1, T2, T3, T4, T, T>(this, new AsyncPipelineHandlersBuilder<T3, T4, T, T>(func));
  }

  public AsyncPipelineHandlersBuilder<T1, T2, T3, T4> Use(AsyncPipelineUnit<T3, T4, T3, T4> func) {
    return new AsyncRecursivePipelineHandlersBuilder<T1, T2, T3, T4, T3, T4>(this, new AsyncPipelineHandlersBuilder<T3, T4, T3, T4>(func));
  }

  public AsyncPipelineHandlersBuilder<T1, T2, T5, T6> Use<T5, T6>(AsyncPipeline<T3, T4, T5, T6> pipeline) {
    return Use(new AsyncAnonymousPipelineUnit<T3, T4, T5, T6>(pipeline));
  }

  public AsyncPipelineHandlersBuilder<T1, T2, T, T> Use<T>(AsyncPipeline<T3, T4, T, T> pipeline) {
    return Use<T, T>(new AsyncAnonymousPipelineUnit<T3, T4, T, T>(pipeline));
  }

  public AsyncPipelineHandlersBuilder<T1, T2, T3, T4> Use(AsyncPipeline<T3, T4, T3, T4> pipeline) {
    return Use(new AsyncAnonymousPipelineUnit<T3, T4, T3, T4>(pipeline));
  }

  public virtual AsyncHandler<T1, T2> End(AsyncHandler<T3, T4> handler) {
    return new AsyncPipelineHandler<T1, T2, T3, T4>(_unit, handler);
  }

}