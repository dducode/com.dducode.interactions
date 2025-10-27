using System.Diagnostics.Contracts;

namespace Interactions.Core.Extensions;

public static class DisposableExtensions {

  [Pure]
  public static IDisposable Compose(this IDisposable first, IDisposable second) {
    return new CompositeDisposable(first, second);
  }

}