using Interactions.Handlers;

namespace Interactions.Builders;

public class AsyncUseHandlersBuilder<T1, T2, T3, T4> {

  private readonly AsyncUse<T1, T2, T3, T4> _delegate;

  internal AsyncUseHandlersBuilder(AsyncUse<T1, T2, T3, T4> func) {
    _delegate = func;
  }

  public AsyncUseHandlersBuilder<T1, T2, T5, T6> Use<T5, T6>(AsyncUse<T3, T4, T5, T6> func) {
    return new AsyncChainedUseHandlersBuilder<T1, T2, T3, T4, T5, T6>(this, new AsyncUseHandlersBuilder<T3, T4, T5, T6>(func));
  }

  public AsyncUseHandlersBuilder<T1, T2, T, T> Use<T>(AsyncUse<T3, T4, T, T> func) {
    return new AsyncChainedUseHandlersBuilder<T1, T2, T3, T4, T, T>(this, new AsyncUseHandlersBuilder<T3, T4, T, T>(func));
  }

  public AsyncUseHandlersBuilder<T1, T2, T3, T4> Use(AsyncUse<T3, T4, T3, T4> func) {
    return new AsyncChainedUseHandlersBuilder<T1, T2, T3, T4, T3, T4>(this, new AsyncUseHandlersBuilder<T3, T4, T3, T4>(func));
  }

  public virtual AsyncHandler<T1, T2> End(AsyncHandler<T3, T4> handler) {
    return new AsyncUseHandler<T1, T2, T3, T4>(_delegate, handler);
  }

}

internal sealed class AsyncChainedUseHandlersBuilder<T1, T2, T3, T4, T5, T6>(
  AsyncUseHandlersBuilder<T1, T2, T3, T4> first,
  AsyncUseHandlersBuilder<T3, T4, T5, T6> second) : AsyncUseHandlersBuilder<T1, T2, T5, T6>(null) {

  public override AsyncHandler<T1, T2> End(AsyncHandler<T5, T6> handler) {
    return first.End(second.End(handler));
  }

}