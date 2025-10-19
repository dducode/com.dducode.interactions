namespace Interactions.Handlers;

internal sealed class AnonymousHandler<T1, T2>(Func<T1, T2> func) : Handler<T1, T2> {

  private bool _disposed;

  protected override T2 HandleCore(T1 input) {
    return _disposed ? throw new ObjectDisposedException(nameof(Func<T1, T2>)) : func(input);
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}

internal sealed class AnonymousHandler<T>(Action<T> action) : Handler<T, Unit> {

  private bool _disposed;

  protected override Unit HandleCore(T input) {
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