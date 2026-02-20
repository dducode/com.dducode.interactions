using System.Diagnostics.Contracts;

namespace Interactions.Core;

public static class Disposable {

  [Pure]
  public static IDisposable Create(Action dispose) {
    return new AnonymousDisposable(dispose);
  }

}