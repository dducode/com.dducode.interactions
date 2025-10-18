namespace Interactions.Handlers;

internal sealed class AnonymousHandler<TIn, TOut> : Handler<TIn, TOut> {

  private readonly Func<TIn, TOut> _func;
  private bool _disposed;

  internal AnonymousHandler(Func<TIn, TOut> func) {
    _func = func;
  }

  protected override TOut HandleCore(TIn input) {
    return _disposed ? throw new ObjectDisposedException(nameof(Func<TIn, TOut>)) : _func(input);
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}

internal sealed class AnonymousHandler<TIn> : Handler<TIn, Unit> {

  private readonly Action<TIn> _action;
  private bool _disposed;

  internal AnonymousHandler(Action<TIn> action) {
    _action = action;
  }

  protected override Unit HandleCore(TIn input) {
    if (!_disposed) {
      _action(input);
      return default;
    }

    throw new ObjectDisposedException(nameof(Action<TIn>));
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}