using System.Diagnostics.Contracts;

namespace Interactions.Core;

public abstract class Handleable<T1, T2> {

  public abstract IDisposable Handle(Handler<T1, T2> handler);

}

public abstract class AsyncHandleable<T1, T2> {

  public abstract IDisposable Handle(AsyncHandler<T1, T2> handler);

}

public static class Handleable {

  [Pure]
  public static Handleable<T1, T2> Create<T1, T2>(Handling<T1, T2> handling) {
    return new AnonymousHandleable<T1, T2>(handling);
  }

  [Pure]
  public static Handleable<T1, T2> FromEvent<T1, T2>(Action<Func<T1, T2>> add, Action<Func<T1, T2>> remove) {
    return Create((Handler<T1, T2> handler) => {
      add(handler.Handle);
      return Disposable.Create(() => {
        try {
          remove(handler.Handle);
        }
        finally {
          handler.Dispose();
        }
      });
    });
  }

  [Pure]
  public static Handleable<T, Unit> FromEvent<T>(Action<Action<T>> add, Action<Action<T>> remove) {
    return Create((Handler<T, Unit> handler) => {
      var action = new Action<T>(i => handler.Handle(i));
      add(action);
      return Disposable.Create(() => {
        try {
          remove(action);
        }
        finally {
          handler.Dispose();
        }
      });
    });
  }

  [Pure]
  public static Handleable<Unit, Unit> FromEvent(Action<Action> add, Action<Action> remove) {
    return Create((Handler<Unit, Unit> handler) => {
      var action = new Action(() => handler.Handle(default));
      add(action);
      return Disposable.Create(() => {
        try {
          remove(action);
        }
        finally {
          handler.Dispose();
        }
      });
    });
  }

}