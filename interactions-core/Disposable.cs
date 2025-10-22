namespace Interactions.Core;

internal class Disposable {

  internal static IDisposable Combine(IDisposable first, IDisposable second) {
    return new CombinedDisposable(first, second);
  }

}