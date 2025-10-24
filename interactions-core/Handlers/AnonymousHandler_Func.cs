namespace Interactions.Core.Handlers;

internal sealed class AnonymousHandler_Func<T1, T2>(Func<T1, T2> func) : Handler<T1, T2> {

  private bool _disposed;

  protected internal override T2 Handle(T1 input) {
    return _disposed ? throw new ObjectDisposedException(nameof(Func<T1, T2>)) : func(input);
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}

internal sealed class AnonymousHandler_Func<T>(Func<T> func) : Handler<Unit, T> {

  private bool _disposed;

  protected internal override T Handle(Unit input) {
    return _disposed ? throw new ObjectDisposedException(nameof(Func<T>)) : func();
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}