using Interactions.Builders;
using Interactions.Handlers;

namespace Interactions;

public abstract partial class AsyncHandler<T1, T2> {

  public static AsyncConditionalHandlersBuilder<T1, T2> If(Func<bool> condition, AsyncHandler<T1, T2> handler) {
    return AsyncConditionalHandlersBuilder<T1, T2>.If(condition, handler);
  }

  public static AsyncConditionalHandlersBuilder<T1, T2> If(Func<bool> condition, Func<T1, CancellationToken, ValueTask<T2>> func) {
    return AsyncConditionalHandlersBuilder<T1, T2>.If(condition, Handler.FromMethod(func));
  }

  public static AsyncUseHandlersBuilder<T1, T2> Use(AsyncUse<T1, T2> func) {
    return new AsyncUseHandlersBuilder<T1, T2>(func);
  }

}