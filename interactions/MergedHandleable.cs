namespace Interactions;

internal sealed class MergedHandleable<T1, T2> : Handleable<T1, T2> {

  private readonly Handleable<T1, T2> _first;
  private readonly Handleable<T1, T2> _second;

  internal MergedHandleable(Handleable<T1, T2> first, Handleable<T1, T2> second) {
    _first = first;
    _second = second;
  }

  public override IDisposable Handle(Handler<T1, T2> handler) {
    return Disposable.Combine(_first.Handle(handler), _second.Handle(handler));
  }

}

internal sealed class AsyncMergedHandleable<T1, T2> : AsyncHandleable<T1, T2> {

  private readonly AsyncHandleable<T1, T2> _first;
  private readonly AsyncHandleable<T1, T2> _second;

  internal AsyncMergedHandleable(AsyncHandleable<T1, T2> first, AsyncHandleable<T1, T2> second) {
    _first = first;
    _second = second;
  }

  public override IDisposable Handle(AsyncHandler<T1, T2> handler) {
    return Disposable.Combine(_first.Handle(handler), _second.Handle(handler));
  }

}