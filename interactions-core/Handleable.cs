namespace Interactions.Core;

public abstract class Handleable<T1, T2> {

  public abstract IDisposable Handle(Handler<T1, T2> handler);

}

public abstract class AsyncHandleable<T1, T2> {

  public abstract IDisposable Handle(AsyncHandler<T1, T2> handler);

}

public static class Handleable {

  public static Handleable<T1, T2> Create<T1, T2>(Handling<T1, T2> handling) {
    return new AnonymousHandleable<T1, T2>(handling);
  }

  public static Handleable<T1, T2> FromEvent<T1, T2>(Action<Func<T1, T2>> add, Action<Func<T1, T2>> remove) {
    return Create((Func<T1, T2> invocation, IDisposable handler) => {
      add(invocation);
      return Disposable.Create(() => {
        handler.Dispose();
        remove(invocation);
      });
    });
  }

  public static Handleable<T, Unit> FromEvent<T>(Action<Action<T>> add, Action<Action<T>> remove) {
    return Create((Func<T, Unit> invocation, IDisposable handler) => {
      var action = new Action<T>(i => invocation(i));
      add(action);
      return Disposable.Create(() => {
        handler.Dispose();
        remove(action);
      });
    });
  }

  public static Handleable<Unit, Unit> FromEvent(Action<Action> add, Action<Action> remove) {
    return Create((Func<Unit, Unit> invocation, IDisposable handler) => {
      var action = new Action(() => invocation(default));
      add(action);
      return Disposable.Create(() => {
        handler.Dispose();
        remove(action);
      });
    });
  }

}