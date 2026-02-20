namespace Interactions.Core;

internal sealed class CompositeDisposable(IDisposable first, IDisposable second) : IDisposable {

  public void Dispose() {
    first.Dispose();
    second.Dispose();
  }

}