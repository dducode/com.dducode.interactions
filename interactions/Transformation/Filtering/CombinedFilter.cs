namespace Interactions.Transformation.Filtering;

internal sealed class CombinedFilter<T>(Filter<T> first, Filter<T> second) : Filter<T> {

  protected override IEnumerable<T> ApplyCore(IEnumerable<T> input) {
    return second.Transform(first.Transform(input));
  }

}