using Interactions.Builders;
using Interactions.Handlers;

namespace Interactions;

public abstract partial class AsyncHandler<TIn, TOut> {

  public static AsyncConditionalHandlersBuilder<TIn, TOut> If(Func<bool> condition, AsyncHandler<TIn, TOut> handler) {
    return AsyncConditionalHandlersBuilder<TIn, TOut>.If(condition, handler);
  }

  public static AsyncConditionalHandlersBuilder<TIn, TOut> If(Func<bool> condition, Func<TIn, CancellationToken, ValueTask<TOut>> func) {
    return AsyncConditionalHandlersBuilder<TIn, TOut>.If(condition, Handler.FromMethod(func));
  }

  public static AsyncUseHandlersBuilder<TIn, TOut> Use(AsyncUse<TIn, TOut> func) {
    return new AsyncUseHandlersBuilder<TIn, TOut>(func);
  }

}