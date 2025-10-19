namespace Interactions.Transformation.Filtering;

internal sealed class Taker<T>(int takeCount) : Filter<T> {

  protected override IEnumerable<T> ApplyCore(IEnumerable<T> input) {
    return input.Take(takeCount);
  }

}