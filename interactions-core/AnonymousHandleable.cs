namespace Interactions.Core;

public delegate IDisposable Handling<out T1, in T2>(Func<T1, T2> invocation, IDisposable handler);

internal sealed class AnonymousHandleable<T1, T2>(Handling<T1, T2> handling) : Handleable<T1, T2> {

  public override IDisposable Handle(Handler<T1, T2> handler) {
    return handling(handler.Handle, handler);
  }

}