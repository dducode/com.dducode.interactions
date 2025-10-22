using System.Diagnostics.Contracts;

namespace Interactions.Core.Extensions;

public static class OptionalExtensions {

  [Pure]
  public static T ValueOrDefault<T>(this Optional<T> optional) {
    return optional.HasValue ? optional.Value : default;
  }

}