namespace Interactions.Transformation.Filtering;

internal sealed class Taker<T>(int takeCount) : Filter<T> {

  protected override IEnumerable<T> Apply(IEnumerable<T> input) {
    return input.Take(takeCount);
  }

}