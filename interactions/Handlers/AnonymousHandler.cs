namespace Interactions.Handlers;

internal sealed class AnonymousHandler<T1, T2> : Handler<T1, T2> {

  private readonly Func<T1, T2> _func;
  private bool _disposed;

  internal AnonymousHandler(Func<T1, T2> func) {
    _func = func;
  }

  protected override T2 HandleCore(T1 input) {
    return _disposed ? throw new ObjectDisposedException(nameof(Func<T1, T2>)) : _func(input);
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}

internal sealed class AnonymousHandler<T> : Handler<T, Unit> {

  private readonly Action<T> _action;
  private bool _disposed;

  internal AnonymousHandler(Action<T> action) {
    _action = action;
  }

  protected override Unit HandleCore(T input) {
    if (!_disposed) {
      _action(input);
      return default;
    }

    throw new ObjectDisposedException(nameof(Action<T>));
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}