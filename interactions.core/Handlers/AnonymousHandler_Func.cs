namespace Interactions.Core.Handlers;

internal sealed class AnonymousHandler_Func<T1, T2>(Func<T1, T2> func) : Handler<T1, T2> {

  private bool _disposed;

  public override T2 Handle(T1 input) {
    return !_disposed ? func(input) : throw new ObjectDisposedException(nameof(AnonymousHandler_Func<T1, T2>));
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}

internal sealed class AnonymousHandler_Func<T>(Func<T> func) : Handler<Unit, T> {

  private bool _disposed;

  public override T Handle(Unit input) {
    return !_disposed ? func() : throw new ObjectDisposedException(nameof(AnonymousHandler_Func<T>));
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}