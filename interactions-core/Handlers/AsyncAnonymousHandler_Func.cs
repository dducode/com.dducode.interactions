namespace Interactions.Core.Handlers;

internal sealed class AsyncAnonymousHandler_Func<T1, T2>(AsyncFunc<T1, T2> func) : AsyncHandler<T1, T2> {

  private bool _disposed;

  protected internal override ValueTask<T2> Handle(T1 input, CancellationToken token = default) {
    return _disposed ? throw new ObjectDisposedException(nameof(AsyncFunc<T1, T2>)) : func(input, token);
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}

internal sealed class AsyncAnonymousHandler_Func<T>(AsyncFunc<T> func) : AsyncHandler<Unit, T> {

  private bool _disposed;

  protected internal override ValueTask<T> Handle(Unit input, CancellationToken token = default) {
    return _disposed ? throw new ObjectDisposedException(nameof(AsyncFunc<T>)) : func(token);
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}