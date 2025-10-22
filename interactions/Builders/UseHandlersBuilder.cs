using Interactions.Handlers;

namespace Interactions.Builders;

public class UseHandlersBuilder<T1, T2, T3, T4> {

  private readonly Use<T1, T2, T3, T4> _delegate;

  internal UseHandlersBuilder(Use<T1, T2, T3, T4> func) {
    _delegate = func;
  }

  public UseHandlersBuilder<T1, T2, T5, T6> Use<T5, T6>(Use<T3, T4, T5, T6> func) {
    return new ChainedUseHandlersBuilder<T1, T2, T3, T4, T5, T6>(this, new UseHandlersBuilder<T3, T4, T5, T6>(func));
  }

  public UseHandlersBuilder<T1, T2, T, T> Use<T>(Use<T3, T4, T, T> func) {
    return new ChainedUseHandlersBuilder<T1, T2, T3, T4, T, T>(this, new UseHandlersBuilder<T3, T4, T, T>(func));
  }

  public UseHandlersBuilder<T1, T2, T3, T4> Use(Use<T3, T4, T3, T4> func) {
    return new ChainedUseHandlersBuilder<T1, T2, T3, T4, T3, T4>(this, new UseHandlersBuilder<T3, T4, T3, T4>(func));
  }

  public virtual Handler<T1, T2> End(Handler<T3, T4> handler) {
    return new UseHandler<T1, T2, T3, T4>(_delegate, handler);
  }

}

internal sealed class ChainedUseHandlersBuilder<T1, T2, T3, T4, T5, T6>(
  UseHandlersBuilder<T1, T2, T3, T4> first,
  UseHandlersBuilder<T3, T4, T5, T6> second) : UseHandlersBuilder<T1, T2, T5, T6>(null) {

  public override Handler<T1, T2> End(Handler<T5, T6> handler) {
    return first.End(second.End(handler));
  }

}