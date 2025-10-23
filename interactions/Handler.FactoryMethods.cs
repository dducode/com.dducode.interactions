using System.Diagnostics.Contracts;
using Interactions.Core;
using Interactions.Core.Handlers;

namespace Interactions;

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
  public static Handler<T, Unit> FromMethod<T>(Action<T> action) {
    return new AnonymousHandler<T>(action);
  }

  [Pure]
  public static Handler<Unit, Unit> FromMethod(Action action) {
    return new AnonymousHandler(action);
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
    return new AnonymousHandler<Unit, bool>(delegate {
      action();
      return true;
    });
  }

}