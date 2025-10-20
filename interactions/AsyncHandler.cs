using Interactions.Handlers;

namespace Interactions;

public abstract partial class AsyncHandler<T1, T2> : IDisposable {

  internal ValueTask<T2> Handle(T1 input, CancellationToken token = default) {
    return HandleCore(input, token);
  }

  protected abstract ValueTask<T2> HandleCore(T1 input, CancellationToken token = default);

  public void Dispose() {
    DisposeCore();
  }

  protected virtual void DisposeCore() {
  }

}

public static class AsyncHandler {

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

  public static AsyncHandler<TIn, bool> AlwaysTrue<TIn>(Func<TIn, ValueTask> action) {
    return new AsyncAnonymousHandler<TIn, bool>(async (input, _) => {
      await action(input);
      return true;
    });
  }

  public static AsyncHandler<TIn, bool> AlwaysTrue<TIn>(Action<TIn> action) {
    return new AsyncAnonymousHandler<TIn, bool>((input, _) => {
      action(input);
      return new ValueTask<bool>(true);
    });
  }

  public static AsyncHandler<Unit, bool> AlwaysTrue(Func<ValueTask> action) {
    return new AsyncAnonymousHandler<Unit, bool>(async (_, _) => {
      await action();
      return true;
    });
  }

  public static AsyncHandler<Unit, bool> AlwaysTrue(Action action) {
    return new AsyncAnonymousHandler<Unit, bool>((_, _) => {
      action();
      return new ValueTask<bool>(true);
    });
  }

}