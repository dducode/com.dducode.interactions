namespace Interactions.Core;

internal class CombinedDisposable(IDisposable first, IDisposable second) : IDisposable {

  public void Dispose() {
    first.Dispose();
    second.Dispose();
  }

}