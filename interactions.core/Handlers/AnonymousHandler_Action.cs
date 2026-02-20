namespace Interactions.Core.Handlers;

internal sealed class AnonymousHandler_Action<T>(Action<T> action) : Handler<T, Unit> {

  private bool _disposed;

  public override Unit Handle(T input) {
    if (_disposed)
      throw new ObjectDisposedException(nameof(AnonymousHandler_Action<T>));

    action(input);
    return default;
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}

internal sealed class AnonymousHandler_Action(Action action) : Handler<Unit, Unit> {

  private bool _disposed;

  public override Unit Handle(Unit input) {
    if (_disposed)
      throw new ObjectDisposedException(nameof(AnonymousHandler_Action));

    action();
    return default;
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}