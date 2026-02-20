namespace Interactions.Core;

public abstract class Handler<T1, T2> : IDisposable {

  public abstract T2 Handle(T1 input);

  public void Dispose() {
    DisposeCore();
  }

  protected virtual void DisposeCore() { }

}