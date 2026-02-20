namespace Interactions;

internal sealed class AnonymousReversibleHandler<T1, T2>(Func<T1, T2> execution, Action<T2> undo, Action<T2> redo) : ReversibleHandler<T1, T2> {

  private bool _disposed;

  public override T2 Handle(T1 input) {
    return !_disposed ? execution(input) : throw new ObjectDisposedException(nameof(AnonymousReversibleHandler<T1, T2>));
  }

  public override void Undo(T2 state) {
    if (_disposed)
      throw new ObjectDisposedException(nameof(AnonymousReversibleHandler<T1, T2>));
    undo(state);
  }

  public override void Redo(T2 state) {
    if (_disposed)
      throw new ObjectDisposedException(nameof(AnonymousReversibleHandler<T1, T2>));
    redo(state);
  }

  protected override void DisposeCore() {
    _disposed = true;
  }

}