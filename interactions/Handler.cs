using Interactions.Handlers;

namespace Interactions;

public abstract partial class Handler<TIn, TOut> : IDisposable {

  internal TOut Handle(TIn input) {
    return HandleCore(input);
  }

  protected abstract TOut HandleCore(TIn input);

  public void Dispose() {
    DisposeCore();
  }

  protected virtual void DisposeCore() {
  }

}

public abstract partial class AsyncHandler<TIn, TOut> : IDisposable {

  internal ValueTask<TOut> Handle(TIn input, CancellationToken token = default) {
    return HandleCore(input, token);
  }

  protected abstract ValueTask<TOut> HandleCore(TIn input, CancellationToken token = default);

  public void Dispose() {
    DisposeCore();
  }

  protected virtual void DisposeCore() {
  }

}

public static class Handler {

  public static AsyncHandler<TIn, TOut> FromMethod<TIn, TOut>(Func<TIn, CancellationToken, ValueTask<TOut>> func) {
    return new AsyncAnonymousHandler<TIn, TOut>(func);
  }

  public static AsyncHandler<Unit, TOut> FromMethod<TOut>(Func<CancellationToken, ValueTask<TOut>> func) {
    return new AsyncAnonymousHandler<Unit, TOut>((_, token) => func(token));
  }

  public static AsyncHandler<TIn, Unit> FromMethod<TIn>(Func<TIn, CancellationToken, ValueTask> func) {
    return new AsyncAnonymousHandler<TIn, Unit>(async (input, token) => {
      await func(input, token);
      return default;
    });
  }

  public static AsyncHandler<Unit, Unit> FromMethod(Func<CancellationToken, ValueTask> func) {
    return new AsyncAnonymousHandler<Unit, Unit>(async (_, token) => {
      await func(token);
      return default;
    });
  }

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

  public static Handler<TIn, bool> FromCommandMethod<TIn>(Action<TIn> action) {
    return new AnonymousHandler<TIn, bool>(input => {
      action(input);
      return true;
    });
  }

  public static Handler<Unit, bool> FromCommandMethod(Action action) {
    return new AnonymousHandler<Unit, bool>(_ => {
      action();
      return true;
    });
  }

}