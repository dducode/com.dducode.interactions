using System.Diagnostics.Contracts;
using Interactions.Transformation.Filtering;

namespace Interactions.Extensions;

public static class FilterExtensions {

  [Pure]
  public static Filter<T> Where<T>(this Filter<T> filter, Func<T, bool> predicate) {
    return filter.Combine(new ConditionalFilter<T>(predicate));
  }

  [Pure]
  public static Filter<T> Distinct<T>(this Filter<T> filter, IEqualityComparer<T> equalityComparer = null) {
    return filter.Combine(equalityComparer == null ? UniqueFilter<T>.Instance : new UniqueFilter<T>(equalityComparer));
  }

  [Pure]
  public static Filter<T> Skip<T>(this Filter<T> filter, int skipCount) {
    return filter.Combine(new Skipper<T>(skipCount));
  }

  [Pure]
  public static Filter<T> Take<T>(this Filter<T> filter, int takeCount) {
    return filter.Combine(new Taker<T>(takeCount));
  }

  [Pure]
  public static Filter<T> Combine<T>(this Filter<T> first, Filter<T> second) {
    return new CombinedFilter<T>(first, second);
  }

}