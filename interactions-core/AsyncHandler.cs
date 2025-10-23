namespace Interactions.Core;

public abstract class AsyncHandler<T1, T2> : IDisposable {

  protected internal abstract ValueTask<T2> Handle(T1 input, CancellationToken token = default);

  public void Dispose() {
    DisposeCore();
  }

  protected virtual void DisposeCore() {
  }

}