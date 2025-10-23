using Interactions.Actions;
using Interactions.Core;
using Interactions.Handlers.Pipeline;

namespace Interactions.Builders;

public class PipelineHandlersBuilder<T1, T2, T3, T4> {

  private readonly PipelineUnit<T1, T2, T3, T4> _unit;

  internal PipelineHandlersBuilder(PipelineUnit<T1, T2, T3, T4> unit) {
    _unit = unit;
  }

  public PipelineHandlersBuilder<T1, T2, T5, T6> Use<T5, T6>(PipelineUnit<T3, T4, T5, T6> unit) {
    return new RecursivePipelineHandlersBuilder<T1, T2, T3, T4, T5, T6>(this, new PipelineHandlersBuilder<T3, T4, T5, T6>(unit));
  }

  public PipelineHandlersBuilder<T1, T2, T, T> Use<T>(PipelineUnit<T3, T4, T, T> unit) {
    return new RecursivePipelineHandlersBuilder<T1, T2, T3, T4, T, T>(this, new PipelineHandlersBuilder<T3, T4, T, T>(unit));
  }

  public PipelineHandlersBuilder<T1, T2, T3, T4> Use(PipelineUnit<T3, T4, T3, T4> unit) {
    return new RecursivePipelineHandlersBuilder<T1, T2, T3, T4, T3, T4>(this, new PipelineHandlersBuilder<T3, T4, T3, T4>(unit));
  }

  public PipelineHandlersBuilder<T1, T2, T5, T6> Use<T5, T6>(Pipeline<T3, T4, T5, T6> pipeline) {
    return Use(new AnonymousPipelineUnit<T3, T4, T5, T6>(pipeline));
  }

  public PipelineHandlersBuilder<T1, T2, T, T> Use<T>(Pipeline<T3, T4, T, T> pipeline) {
    return Use<T, T>(new AnonymousPipelineUnit<T3, T4, T, T>(pipeline));
  }

  public PipelineHandlersBuilder<T1, T2, T3, T4> Use(Pipeline<T3, T4, T3, T4> pipeline) {
    return Use(new AnonymousPipelineUnit<T3, T4, T3, T4>(pipeline));
  }

  public virtual Handler<T1, T2> End(Handler<T3, T4> handler) {
    return new PipelineHandler<T1, T2, T3, T4>(_unit, handler);
  }

}