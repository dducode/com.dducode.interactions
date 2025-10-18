using System.Diagnostics.Contracts;

namespace Interactions.Transformation.Filtering;

public abstract class Filter<T> : Transformer<IEnumerable<T>, IEnumerable<T>> {

  public static Filter<T> operator +(Filter<T> first, Filter<T> second) {
    return new CombinedFilter<T>(first, second);
  }

  protected override IEnumerable<T> TransformCore(IEnumerable<T> input) {
    return ApplyCore(input);
  }

  protected abstract IEnumerable<T> ApplyCore(IEnumerable<T> input);

}

public static class Filter {

  [Pure]
  public static Filter<T> Identity<T>() {
    return IdentityFilter<T>.Instance;
  }

  [Pure]
  public static Filter<T> Where<T>(Func<T, bool> predicate) {
    return new ConditionalFilter<T>(predicate);
  }

  [Pure]
  public static Filter<T> Distinct<T>(IEqualityComparer<T> equalityComparer = null) {
    return equalityComparer == null ? UniqueFilter<T>.Instance : new UniqueFilter<T>(equalityComparer);
  }

  [Pure]
  public static Filter<T> Skip<T>(int skipCount) {
    return new Skipper<T>(skipCount);
  }

  [Pure]
  public static Filter<T> Take<T>(int takeCount) {
    return new Taker<T>(takeCount);
  }

  [Pure]
  public static Filter<T> FromMethod<T>(Func<IEnumerable<T>, IEnumerable<T>> filtration) {
    return new AnonymousFilter<T>(filtration);
  }

}