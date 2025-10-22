namespace Interactions.Transformation.Filtering;

internal sealed class CompositeFilter<T>(Filter<T> first, Filter<T> second) : Filter<T> {

  protected override IEnumerable<T> Apply(IEnumerable<T> input) {
    return second.Transform(first.Transform(input));
  }

}