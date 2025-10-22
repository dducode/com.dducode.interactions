using Interactions.Core.Handlers;

namespace Interactions.Core.Extensions;

public static class ConditionalHandlersBuilderExtensions {

  public static ConditionalHandlersBuilder<T1, T2> ElseIf<T1, T2>(
    this ConditionalHandlersBuilder<T1, T2> builder, Func<bool> condition, Func<T1, T2> action) {
    return builder.ElseIf(condition, new AnonymousHandler<T1, T2>(action));
  }

  public static Handler<T1, T2> Else<T1, T2>(this ConditionalHandlersBuilder<T1, T2> builder, Func<T1, T2> action) {
    return builder.Else(new AnonymousHandler<T1, T2>(action));
  }

  public static AsyncConditionalHandlersBuilder<T1, T2> ElseIf<T1, T2>(
    this AsyncConditionalHandlersBuilder<T1, T2> builder, Func<bool> condition, Func<T1, CancellationToken, ValueTask<T2>> action) {
    return builder.ElseIf(condition, new AsyncAnonymousHandler<T1, T2>(action));
  }

  public static AsyncHandler<T1, T2> Else<T1, T2>(
    this AsyncConditionalHandlersBuilder<T1, T2> builder, Func<T1, CancellationToken, ValueTask<T2>> action) {
    return builder.Else(new AsyncAnonymousHandler<T1, T2>(action));
  }

}