namespace Interactions.Handlers;

internal sealed class AsyncAnonymousHandler<T1, T2> : AsyncHandler<T1, T2> {

  private readonly Func<T1, CancellationToken, ValueTask<T2>> _func;
  private bool _disposed;

  internal AsyncAnonymousHandler(Func<T1, CancellationToken, ValueTask<T2>> func) {
    _func = func;
  }

  protected override ValueTask<T2> HandleCore(T1 input, CancellationToken token = default) {
    return _disposed ? throw new ObjectDisposedException(nameof(Func<T1, CancellationToken, ValueTask<T2>>)) : _func(input, token);
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}

internal sealed class AsyncAnonymousHandler<T> : AsyncHandler<T, Unit> {

  private readonly Func<T, CancellationToken, ValueTask> _func;
  private bool _disposed;

  internal AsyncAnonymousHandler(Func<T, CancellationToken, ValueTask> func) {
    _func = func;
  }

  protected override async ValueTask<Unit> HandleCore(T input, CancellationToken token = default) {
    if (!_disposed) {
      await _func(input, token);
      return default;
    }

    throw new ObjectDisposedException(nameof(Func<T, CancellationToken, ValueTask>));
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}