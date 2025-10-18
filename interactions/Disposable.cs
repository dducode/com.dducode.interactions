namespace Interactions;

internal class Disposable {

  public static IDisposable Combine(IDisposable first, IDisposable second) {
    return new CombinedDisposable(first, second);
  }

}