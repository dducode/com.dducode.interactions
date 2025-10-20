using Interactions.Handlers;

namespace Interactions;

public abstract partial class Handler<T1, T2> : IDisposable {

  internal T2 Handle(T1 input) {
    return HandleCore(input);
  }

  protected abstract T2 HandleCore(T1 input);

  public void Dispose() {
    DisposeCore();
  }

  protected virtual void DisposeCore() {
  }

}

public static class Handler {

  public static Handler<TIn, TOut> FromMethod<TIn, TOut>(Func<TIn, TOut> func) {
    return new AnonymousHandler<TIn, TOut>(func);
  }

  public static Handler<Unit, TOut> FromMethod<TOut>(Func<TOut> func) {
    return new AnonymousHandler<Unit, TOut>(_ => func());
  }

  public static Handler<TIn, Unit> FromMethod<TIn>(Action<TIn> action) {
    return new AnonymousHandler<TIn, Unit>(input => {
      action(input);
      return default;
    });
  }

  public static Handler<Unit, Unit> FromMethod(Action action) {
    return new AnonymousHandler<Unit, Unit>(_ => {
      action();
      return default;
    });
  }

  public static Handler<TIn, bool> AlwaysTrue<TIn>(Action<TIn> action) {
    return new AnonymousHandler<TIn, bool>(input => {
      action(input);
      return true;
    });
  }

  public static Handler<Unit, bool> AlwaysTrue(Action action) {
    return new AnonymousHandler<Unit, bool>(_ => {
      action();
      return true;
    });
  }

}