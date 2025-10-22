using System.Diagnostics.Contracts;
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

  [Pure]
  public static Handler<T, T> Identity<T>() {
    return IdentityHandler<T>.Instance;
  }

  [Pure]
  public static Handler<T1, T2> FromMethod<T1, T2>(Func<T1, T2> func) {
    return new AnonymousHandler<T1, T2>(func);
  }

  [Pure]
  public static Handler<T, T> FromMethod<T>(Func<T, T> func) {
    return FromMethod<T, T>(func);
  }

  [Pure]
  public static Handler<Unit, T> FromMethod<T>(Func<T> func) {
    return new AnonymousHandler<Unit, T>(_ => func());
  }

  [Pure]
  public static Handler<T, Unit> FromMethod<T>(Action<T> action) {
    return new AnonymousHandler<T, Unit>(input => {
      action(input);
      return default;
    });
  }

  [Pure]
  public static Handler<Unit, Unit> FromMethod(Action action) {
    return new AnonymousHandler<Unit, Unit>(_ => {
      action();
      return default;
    });
  }

  [Pure]
  public static Handler<T, bool> AlwaysTrue<T>(Action<T> action) {
    return new AnonymousHandler<T, bool>(input => {
      action(input);
      return true;
    });
  }

  [Pure]
  public static Handler<Unit, bool> AlwaysTrue(Action action) {
    return new AnonymousHandler<Unit, bool>(_ => {
      action();
      return true;
    });
  }

}