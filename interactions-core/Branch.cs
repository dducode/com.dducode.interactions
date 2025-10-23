using Interactions.Core.Handlers;

namespace Interactions.Core;

public static class Branch<T1, T2> {

  public static ConditionalHandlersBuilder<T1, T2> If(Func<bool> condition, Handler<T1, T2> handler) {
    return ConditionalHandlersBuilder<T1, T2>.If(condition, handler);
  }

  public static ConditionalHandlersBuilder<T1, T2> If(Func<bool> condition, Func<T1, T2> func) {
    return If(condition, new AnonymousHandler<T1, T2>(func));
  }

  public static AsyncConditionalHandlersBuilder<T1, T2> If(Func<bool> condition, AsyncHandler<T1, T2> handler) {
    return AsyncConditionalHandlersBuilder<T1, T2>.If(condition, handler);
  }

  public static AsyncConditionalHandlersBuilder<T1, T2> If(Func<bool> condition, Func<T1, CancellationToken, ValueTask<T2>> func) {
    return If(condition, new AsyncAnonymousHandler<T1, T2>(func));
  }

}

public static class Branch<T> {

  public static ConditionalHandlersBuilder<T, Unit> If(Func<bool> condition, Handler<T, Unit> handler) {
    return ConditionalHandlersBuilder<T, Unit>.If(condition, handler);
  }

  public static ConditionalHandlersBuilder<T, Unit> If(Func<bool> condition, Action<T> action) {
    return If(condition, new AnonymousHandler<T>(action));
  }

  public static AsyncConditionalHandlersBuilder<T, Unit> If(Func<bool> condition, AsyncHandler<T, Unit> handler) {
    return AsyncConditionalHandlersBuilder<T, Unit>.If(condition, handler);
  }

  public static AsyncConditionalHandlersBuilder<T, Unit> If(Func<bool> condition, Func<T, CancellationToken, ValueTask> action) {
    return If(condition, new AsyncAnonymousHandler<T>(action));
  }

}

public static class Branch {

  public static ConditionalHandlersBuilder<Unit, Unit> If(Func<bool> condition, Handler<Unit, Unit> handler) {
    return ConditionalHandlersBuilder<Unit, Unit>.If(condition, handler);
  }

  public static ConditionalHandlersBuilder<Unit, Unit> If(Func<bool> condition, Action action) {
    return If(condition, new AnonymousHandler(action));
  }

  public static AsyncConditionalHandlersBuilder<Unit, Unit> If(Func<bool> condition, AsyncHandler<Unit, Unit> handler) {
    return AsyncConditionalHandlersBuilder<Unit, Unit>.If(condition, handler);
  }

  public static AsyncConditionalHandlersBuilder<Unit, Unit> If(Func<bool> condition, Func<CancellationToken, ValueTask> action) {
    return If(condition, new AsyncAnonymousHandler(action));
  }

}