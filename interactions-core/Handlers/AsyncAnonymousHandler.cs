namespace Interactions.Core.Handlers;

internal sealed class AsyncAnonymousHandler<T1, T2>(Func<T1, CancellationToken, ValueTask<T2>> func) : AsyncHandler<T1, T2> {

  private bool _disposed;

  protected internal override ValueTask<T2> Handle(T1 input, CancellationToken token = default) {
    return _disposed ? throw new ObjectDisposedException(nameof(Func<T1, CancellationToken, ValueTask<T2>>)) : func(input, token);
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}

internal sealed class AsyncAnonymousHandler<T>(Func<T, CancellationToken, ValueTask> func) : AsyncHandler<T, Unit> {

  private bool _disposed;

  protected internal override async ValueTask<Unit> Handle(T input, CancellationToken token = default) {
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

internal sealed class AsyncAnonymousHandler(Func<CancellationToken, ValueTask> func) : AsyncHandler<Unit, Unit> {

  private bool _disposed;

  protected internal override async ValueTask<Unit> Handle(Unit input, CancellationToken token = default) {
    if (!_disposed) {
      await func(token);
      return default;
    }

    throw new ObjectDisposedException(nameof(Func<CancellationToken, ValueTask>));
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}