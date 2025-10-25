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
    return new AnonymousHandler_Func<T1, T2>(func);
  }

  [Pure]
  public static Handler<T, T> FromMethod<T>(Func<T, T> func) {
    return FromMethod<T, T>(func);
  }

  [Pure]
  public static Handler<Unit, T> FromMethod<T>(Func<T> action) {
    return new AnonymousHandler_Func<T>(action);
  }

  public static Handler<T, Unit> FromMethod<T>(Action<T> action) {
    return new AnonymousHandler_Action<T>(action);
  }

  [Pure]
  public static Handler<Unit, Unit> FromMethod(Action action) {
    return new AnonymousHandler_Action(action);
  }

  [Pure]
  public static Handler<T, bool> AlwaysTrue<T>(Action<T> action) {
    return new AnonymousHandler_Func<T, bool>(input => {
      action(input);
      return true;
    });
  }

  [Pure]
  public static Handler<Unit, bool> AlwaysTrue(Action action) {
    return new AnonymousHandler_Func<bool>(delegate {
      action();
      return true;
    });
  }

  [Pure]
  public static AsyncHandler<T1, T2> FromMethod<T1, T2>(AsyncFunc<T1, T2> func) {
    return new AsyncAnonymousHandler_Func<T1, T2>(func);
  }

  [Pure]
  public static AsyncHandler<T, T> FromMethod<T>(AsyncFunc<T, T> func) {
    return FromMethod<T, T>(func);
  }

  [Pure]
  public static AsyncHandler<Unit, T> FromMethod<T>(AsyncFunc<T> action) {
    return new AsyncAnonymousHandler_Func<T>(action);
  }

  [Pure]
  public static AsyncHandler<T, Unit> FromMethod<T>(AsyncAction<T> action) {
    return new AsyncAnonymousHandler_Action<T>(action);
  }

  [Pure]
  public static AsyncHandler<Unit, Unit> FromMethod(AsyncAction action) {
    return new AsyncAnonymousHandler_Action(action);
  }

  [Pure]
  public static AsyncHandler<T, bool> AlwaysTrue<T>(AsyncAction<T> action) {
    return new AsyncAnonymousHandler_Func<T, bool>(async (input, token) => {
      await action(input, token);
      return true;
    });
  }

  [Pure]
  public static AsyncHandler<Unit, bool> AlwaysTrue(AsyncAction action) {
    return new AsyncAnonymousHandler_Func<bool>(async token => {
      await action(token);
      return true;
    });
  }

}