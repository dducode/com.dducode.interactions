using Interactions.Builders;

namespace Interactions.Extensions;

public static class ConditionalHandlersBuilderExtensions {

  public static ConditionalHandlersBuilder<T1, T2> ElseIf<T1, T2>(
    this ConditionalHandlersBuilder<T1, T2> builder, Func<bool> condition, Func<T1, T2> action) {
    return builder.ElseIf(condition, Handler.FromMethod(action));
  }

  public static Handler<T1, T2> Else<T1, T2>(this ConditionalHandlersBuilder<T1, T2> builder, Func<T1, T2> action) {
    return builder.Else(Handler.FromMethod(action));
  }

  public static AsyncConditionalHandlersBuilder<T1, T2> ElseIf<T1, T2>(
    this AsyncConditionalHandlersBuilder<T1, T2> builder, Func<bool> condition, Func<T1, CancellationToken, ValueTask<T2>> action) {
    return builder.ElseIf(condition, AsyncHandler.FromMethod(action));
  }

  public static AsyncHandler<T1, T2> Else<T1, T2>(
    this AsyncConditionalHandlersBuilder<T1, T2> builder, Func<T1, CancellationToken, ValueTask<T2>> action) {
    return builder.Else(AsyncHandler.FromMethod(action));
  }

}