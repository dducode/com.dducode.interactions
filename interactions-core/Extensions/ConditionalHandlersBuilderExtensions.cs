using Interactions.Core.Handlers;

namespace Interactions.Core.Extensions;

public static partial class ConditionalHandlersBuilderExtensions {

  public static ConditionalHandlersBuilder<T1, T2> ElseIf<T1, T2>(
    this ConditionalHandlersBuilder<T1, T2> builder, Func<bool> condition, Func<T1, T2> action) {
    return builder.ElseIf(condition, new AnonymousHandler<T1, T2>(action));
  }

  public static ConditionalHandlersBuilder<T, Unit> ElseIf<T>(
    this ConditionalHandlersBuilder<T, Unit> builder, Func<bool> condition, Action<T> action) {
    return builder.ElseIf(condition, new AnonymousHandler<T>(action));
  }

  public static ConditionalHandlersBuilder<Unit, Unit> ElseIf(
    this ConditionalHandlersBuilder<Unit, Unit> builder, Func<bool> condition, Action action) {
    return builder.ElseIf(condition, new AnonymousHandler(action));
  }

  public static Handler<T1, T2> Else<T1, T2>(this ConditionalHandlersBuilder<T1, T2> builder, Func<T1, T2> action) {
    return builder.Else(new AnonymousHandler<T1, T2>(action));
  }

  public static Handler<T, Unit> Else<T>(this ConditionalHandlersBuilder<T, Unit> builder, Action<T> action) {
    return builder.Else(new AnonymousHandler<T>(action));
  }

  public static Handler<Unit, Unit> Else(this ConditionalHandlersBuilder<Unit, Unit> builder, Action action) {
    return builder.Else(new AnonymousHandler(action));
  }

}