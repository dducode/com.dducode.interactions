namespace Interactions.Transformation.Filtering;

internal sealed class ConditionalFilter<T>(Func<T, bool> predicate) : Filter<T> {

  protected override IEnumerable<T> ApplyCore(IEnumerable<T> input) {
    return input.Where(predicate);
  }

}