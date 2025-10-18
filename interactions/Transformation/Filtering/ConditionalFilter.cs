namespace Interactions.Transformation.Filtering;

internal sealed class ConditionalFilter<T> : Filter<T> {

  private readonly Func<T, bool> _predicate;

  internal ConditionalFilter(Func<T, bool> predicate) {
    _predicate = predicate;
  }

  protected override IEnumerable<T> ApplyCore(IEnumerable<T> input) {
    return input.Where(_predicate);
  }

}