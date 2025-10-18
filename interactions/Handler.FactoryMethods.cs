using Interactions.Builders;
using Interactions.Handlers;

namespace Interactions;

public abstract partial class Handler<TIn, TOut> {

  public static ConditionalHandlersBuilder<TIn, TOut> If(Func<bool> condition, Handler<TIn, TOut> handler) {
    return ConditionalHandlersBuilder<TIn, TOut>.If(condition, handler);
  }

  public static ConditionalHandlersBuilder<TIn, TOut> If(Func<bool> condition, Func<TIn, TOut> func) {
    return ConditionalHandlersBuilder<TIn, TOut>.If(condition, Handler.FromMethod(func));
  }

  public static UseHandlersBuilder<TIn, TOut> Use(Use<TIn, TOut> func) {
    return new UseHandlersBuilder<TIn, TOut>(func);
  }

}