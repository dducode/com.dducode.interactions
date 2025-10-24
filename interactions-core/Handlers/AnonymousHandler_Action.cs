namespace Interactions.Core.Handlers;

internal sealed class AnonymousHandler_Action<T>(Action<T> action) : Handler<T, Unit> {

  private bool _disposed;

  protected internal override Unit Handle(T input) {
    if (!_disposed) {
      action(input);
      return default;
    }

    throw new ObjectDisposedException(nameof(Action<T>));
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}

internal sealed class AnonymousHandler_Action(Action action) : Handler<Unit, Unit> {

  private bool _disposed;

  protected internal override Unit Handle(Unit input) {
    if (!_disposed) {
      action();
      return default;
    }

    throw new ObjectDisposedException(nameof(Action));
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}