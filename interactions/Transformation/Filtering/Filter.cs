using System.Diagnostics.Contracts;
using Interactions.Validation;

namespace Interactions.Transformation.Filtering;

public abstract class Filter<T> : Transformer<IEnumerable<T>, IEnumerable<T>> {

  protected internal override IEnumerable<T> Transform(IEnumerable<T> input) {
    return Apply(input);
  }

  protected abstract IEnumerable<T> Apply(IEnumerable<T> input);

}

public static class Filter {

  [Pure]
  public static Filter<T> Identity<T>() {
    return IdentityFilter<T>.Instance;
  }

  [Pure]
  public static Filter<T> Where<T>(Validator<T> validator) {
    return new ConditionalFilter<T>(validator);
  }

  [Pure]
  public static Filter<T> Where<T>(Func<T, bool> predicate) {
    return Where(Validator.FromMethod(predicate, string.Empty));
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