using Interactions.Builders;

namespace Interactions.Extensions;

public static class ConditionalHandlersBuilderExtensions {

  public static ConditionalHandlersBuilder<TIn, TOut> ElseIf<TIn, TOut>(
    this ConditionalHandlersBuilder<TIn, TOut> builder, Func<bool> condition, Func<TIn, TOut> action) {
    return builder.ElseIf(condition, Handler.FromMethod(action));
  }

  public static Handler<TIn, TOut> Else<TIn, TOut>(this ConditionalHandlersBuilder<TIn, TOut> builder, Func<TIn, TOut> action) {
    return builder.Else(Handler.FromMethod(action));
  }

  public static AsyncConditionalHandlersBuilder<TIn, TOut> ElseIf<TIn, TOut>(
    this AsyncConditionalHandlersBuilder<TIn, TOut> builder, Func<bool> condition, Func<TIn, CancellationToken, ValueTask<TOut>> action) {
    return builder.ElseIf(condition, Handler.FromMethod(action));
  }

  public static AsyncHandler<TIn, TOut> Else<TIn, TOut>(
    this AsyncConditionalHandlersBuilder<TIn, TOut> builder, Func<TIn, CancellationToken, ValueTask<TOut>> action) {
    return builder.Else(Handler.FromMethod(action));
  }

}