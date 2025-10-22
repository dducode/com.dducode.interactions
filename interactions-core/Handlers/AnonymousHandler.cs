namespace Interactions.Core.Handlers;

internal sealed class AnonymousHandler<T1, T2>(Func<T1, T2> func) : Handler<T1, T2> {

  private bool _disposed;

  protected internal override T2 Handle(T1 input) {
    return _disposed ? throw new ObjectDisposedException(nameof(Func<T1, T2>)) : func(input);
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}

internal sealed class AnonymousHandler<T>(Action<T> action) : Handler<T, Unit> {

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

internal sealed class AnonymousHandler(Action action) : Handler<Unit, Unit> {

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