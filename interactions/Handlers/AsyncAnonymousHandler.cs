namespace Interactions.Handlers;

internal sealed class AsyncAnonymousHandler<T1, T2>(Func<T1, CancellationToken, ValueTask<T2>> func) : AsyncHandler<T1, T2> {

  private bool _disposed;

  protected override ValueTask<T2> HandleCore(T1 input, CancellationToken token = default) {
    return _disposed ? throw new ObjectDisposedException(nameof(Func<T1, CancellationToken, ValueTask<T2>>)) : func(input, token);
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}

internal sealed class AsyncAnonymousHandler<T>(Func<T, CancellationToken, ValueTask> func) : AsyncHandler<T, Unit> {

  private bool _disposed;

  protected override async ValueTask<Unit> HandleCore(T input, CancellationToken token = default) {
    if (!_disposed) {
      await func(input, token);
      return default;
    }

    throw new ObjectDisposedException(nameof(Func<T, CancellationToken, ValueTask>));
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}