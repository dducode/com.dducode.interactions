namespace Interactions;

internal sealed class MergedHandleable<T1, T2>(Handleable<T1, T2> first, Handleable<T1, T2> second) : Handleable<T1, T2> {

  public override IDisposable Handle(Handler<T1, T2> handler) {
    return Disposable.Combine(first.Handle(handler), second.Handle(handler));
  }

}

internal sealed class AsyncMergedHandleable<T1, T2>(AsyncHandleable<T1, T2> first, AsyncHandleable<T1, T2> second) : AsyncHandleable<T1, T2> {

  public override IDisposable Handle(AsyncHandler<T1, T2> handler) {
    return Disposable.Combine(first.Handle(handler), second.Handle(handler));
  }

}