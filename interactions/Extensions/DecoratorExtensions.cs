using System.Diagnostics.Contracts;

namespace Interactions.Extensions;

public static class DecoratorExtensions {

  [Pure]
  public static Decorator<T1, T3> Combine<T1, T2, T3>(this Decorator<T1, T2> first, Decorator<T2, T3> second) {
    return new CombinedDecorator<T1, T2, T3>(first, second);
  }

}