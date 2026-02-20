using System.Diagnostics.Contracts;

namespace Interactions.Extensions;

public static class DecoratorExtensions {

  [Pure]
  public static Decorator<T1, T3> Compose<T1, T2, T3>(this Decorator<T1, T2> first, Decorator<T2, T3> second) {
    return new CompositeDecorator<T1, T2, T3>(first, second);
  }

}