namespace Interactions.Core.Handlers;

internal sealed class AsyncAnonymousHandler_Action<T>(AsyncAction<T> action) : AsyncHandler<T, Unit> {

  private bool _disposed;

  public override async ValueTask<Unit> Handle(T input, CancellationToken token = default) {
    if (_disposed)
      throw new ObjectDisposedException(nameof(AsyncAnonymousHandler_Action<T>));

    await action(input, token);
    return default;
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}

internal sealed class AsyncAnonymousHandler_Action(AsyncAction action) : AsyncHandler<Unit, Unit> {

  private bool _disposed;

  public override async ValueTask<Unit> Handle(Unit input, CancellationToken token = default) {
    if (_disposed)
      throw new ObjectDisposedException(nameof(AsyncAnonymousHandler_Action));

    await action(token);
    return default;
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}