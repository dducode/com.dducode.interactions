namespace Interactions.Handlers;

internal sealed class AsyncAnonymousHandler<TIn, TOut> : AsyncHandler<TIn, TOut> {

  private readonly Func<TIn, CancellationToken, ValueTask<TOut>> _func;
  private bool _disposed;

  internal AsyncAnonymousHandler(Func<TIn, CancellationToken, ValueTask<TOut>> func) {
    _func = func;
  }

  protected override ValueTask<TOut> HandleCore(TIn input, CancellationToken token = default) {
    return _disposed ? throw new ObjectDisposedException(nameof(Func<TIn, CancellationToken, ValueTask<TOut>>)) : _func(input, token);
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}

internal sealed class AsyncAnonymousHandler<TIn> : AsyncHandler<TIn, Unit> {

  private readonly Func<TIn, CancellationToken, ValueTask> _func;
  private bool _disposed;

  internal AsyncAnonymousHandler(Func<TIn, CancellationToken, ValueTask> func) {
    _func = func;
  }

  protected override async ValueTask<Unit> HandleCore(TIn input, CancellationToken token = default) {
    if (!_disposed) {
      await _func(input, token);
      return default;
    }

    throw new ObjectDisposedException(nameof(Func<TIn, CancellationToken, ValueTask>));
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}