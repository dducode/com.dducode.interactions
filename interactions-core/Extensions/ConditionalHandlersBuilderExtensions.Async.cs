using Interactions.Core.Handlers;

namespace Interactions.Core.Extensions;

public static partial class ConditionalHandlersBuilderExtensions {

  public static AsyncConditionalHandlersBuilder<T1, T2> ElseIf<T1, T2>(
    this AsyncConditionalHandlersBuilder<T1, T2> builder, Func<bool> condition, Func<T1, CancellationToken, ValueTask<T2>> action) {
    return builder.ElseIf(condition, new AsyncAnonymousHandler<T1, T2>(action));
  }

  public static AsyncConditionalHandlersBuilder<T, Unit> ElseIf<T>(
    this AsyncConditionalHandlersBuilder<T, Unit> builder, Func<bool> condition, Func<T, CancellationToken, ValueTask> action) {
    return builder.ElseIf(condition, new AsyncAnonymousHandler<T>(action));
  }

  public static AsyncConditionalHandlersBuilder<Unit, Unit> ElseIf(
    this AsyncConditionalHandlersBuilder<Unit, Unit> builder, Func<bool> condition, Func<CancellationToken, ValueTask> action) {
    return builder.ElseIf(condition, new AsyncAnonymousHandler(action));
  }

  public static AsyncHandler<T1, T2> Else<T1, T2>(
    this AsyncConditionalHandlersBuilder<T1, T2> builder, Func<T1, CancellationToken, ValueTask<T2>> action) {
    return builder.Else(new AsyncAnonymousHandler<T1, T2>(action));
  }

  public static AsyncHandler<T, Unit> Else<T>(this AsyncConditionalHandlersBuilder<T, Unit> builder, Func<T, CancellationToken, ValueTask> action) {
    return builder.Else(new AsyncAnonymousHandler<T>(action));
  }

  public static AsyncHandler<Unit, Unit> Else(this AsyncConditionalHandlersBuilder<Unit, Unit> builder, Func<CancellationToken, ValueTask> action) {
    return builder.Else(new AsyncAnonymousHandler(action));
  }

}