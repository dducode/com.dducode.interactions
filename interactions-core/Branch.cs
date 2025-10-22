using Interactions.Core.Handlers;

namespace Interactions.Core;

public static class Branch<T1, T2> {

  public static ConditionalHandlersBuilder<T1, T2> If(Func<bool> condition, Handler<T1, T2> handler) {
    return ConditionalHandlersBuilder<T1, T2>.If(condition, handler);
  }

  public static ConditionalHandlersBuilder<T1, T2> If(Func<bool> condition, Func<T1, T2> func) {
    return ConditionalHandlersBuilder<T1, T2>.If(condition, new AnonymousHandler<T1, T2>(func));
  }

  public static AsyncConditionalHandlersBuilder<T1, T2> If(Func<bool> condition, AsyncHandler<T1, T2> handler) {
    return AsyncConditionalHandlersBuilder<T1, T2>.If(condition, handler);
  }

  public static AsyncConditionalHandlersBuilder<T1, T2> If(Func<bool> condition, Func<T1, CancellationToken, ValueTask<T2>> func) {
    return AsyncConditionalHandlersBuilder<T1, T2>.If(condition, new AsyncAnonymousHandler<T1, T2>(func));
  }

}