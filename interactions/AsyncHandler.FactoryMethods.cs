using System.Diagnostics.Contracts;
using Interactions.Core;
using Interactions.Core.Handlers;

namespace Interactions;

public static class AsyncHandler {

  [Pure]
  public static AsyncHandler<T, T> Identity<T>() {
    return AsyncIdentityHandler<T>.Instance;
  }

  [Pure]
  public static AsyncHandler<T1, T2> FromMethod<T1, T2>(Func<T1, CancellationToken, ValueTask<T2>> func) {
    return new AsyncAnonymousHandler<T1, T2>(func);
  }

  [Pure]
  public static AsyncHandler<T, T> FromMethod<T>(Func<T, CancellationToken, ValueTask<T>> func) {
    return FromMethod<T, T>(func);
  }

  [Pure]
  public static AsyncHandler<Unit, T> FromMethod<T>(Func<CancellationToken, ValueTask<T>> func) {
    return new AsyncAnonymousHandler<Unit, T>((_, token) => func(token));
  }

  [Pure]
  public static AsyncHandler<T, Unit> FromMethod<T>(Func<T, CancellationToken, ValueTask> func) {
    return new AsyncAnonymousHandler<T>(func);
  }

  [Pure]
  public static AsyncHandler<Unit, Unit> FromMethod(Func<CancellationToken, ValueTask> func) {
    return new AsyncAnonymousHandler(func);
  }

  [Pure]
  public static AsyncHandler<T, bool> AlwaysTrue<T>(Func<T, ValueTask> action) {
    return new AsyncAnonymousHandler<T, bool>(async (input, _) => {
      await action(input);
      return true;
    });
  }

  [Pure]
  public static AsyncHandler<T, bool> AlwaysTrue<T>(Action<T> action) {
    return new AsyncAnonymousHandler<T, bool>((input, _) => {
      action(input);
      return new ValueTask<bool>(true);
    });
  }

  [Pure]
  public static AsyncHandler<Unit, bool> AlwaysTrue(Func<ValueTask> action) {
    return new AsyncAnonymousHandler<Unit, bool>(async (_, _) => {
      await action();
      return true;
    });
  }

  [Pure]
  public static AsyncHandler<Unit, bool> AlwaysTrue(Action action) {
    return new AsyncAnonymousHandler<Unit, bool>((_, _) => {
      action();
      return new ValueTask<bool>(true);
    });
  }

}