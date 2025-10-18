namespace Interactions.Transformation.Filtering;

internal sealed class AnonymousFilter<T> : Filter<T> {

  private readonly Func<IEnumerable<T>, IEnumerable<T>> _filtration;

  internal AnonymousFilter(Func<IEnumerable<T>, IEnumerable<T>> filtration) {
    _filtration = filtration;
  }

  protected override IEnumerable<T> ApplyCore(IEnumerable<T> input) {
    return _filtration(input);
  }

}