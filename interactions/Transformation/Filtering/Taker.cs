namespace Interactions.Transformation.Filtering;

internal sealed class Taker<T> : Filter<T> {

  private readonly int _takeCount;

  internal Taker(int takeCount) {
    _takeCount = takeCount;
  }

  protected override IEnumerable<T> ApplyCore(IEnumerable<T> input) {
    return input.Take(_takeCount);
  }

}