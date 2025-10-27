namespace Interactions.Core;

public sealed class Disposable : IDisposable {

  private readonly Action _dispose;

  public static Disposable Create(Action dispose) {
    return new Disposable(dispose);
  }

  private Disposable(Action dispose) {
    _dispose = dispose;
  }

  public void Dispose() {
    _dispose();
  }

}