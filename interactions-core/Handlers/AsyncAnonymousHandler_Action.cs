namespace Interactions.Core.Handlers;

internal sealed class AsyncAnonymousHandler_Action<T>(AsyncAction<T> action) : AsyncHandler<T, Unit> {

  private bool _disposed;

  protected internal override async ValueTask<Unit> Handle(T input, CancellationToken token = default) {
    if (!_disposed) {
      await action(input, token);
      return default;
    }

    throw new ObjectDisposedException(nameof(AsyncAction<T>));
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}

internal sealed class AsyncAnonymousHandler_Action(AsyncAction action) : AsyncHandler<Unit, Unit> {

  private bool _disposed;

  protected internal override async ValueTask<Unit> Handle(Unit input, CancellationToken token = default) {
    if (!_disposed) {
      await action(token);
      return default;
    }

    throw new ObjectDisposedException(nameof(AsyncAction));
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}