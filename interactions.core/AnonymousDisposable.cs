namespace Interactions.Core;

internal sealed class AnonymousDisposable(Action dispose) : IDisposable {

  public void Dispose() {
    dispose();
  }

}