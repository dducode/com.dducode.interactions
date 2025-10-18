namespace Interactions.Transformation.Filtering;

internal sealed class CombinedFilter<T> : Filter<T> {

  private readonly Filter<T> _first;
  private readonly Filter<T> _second;

  internal CombinedFilter(Filter<T> first, Filter<T> second) {
    _first = first;
    _second = second;
  }

  protected override IEnumerable<T> ApplyCore(IEnumerable<T> input) {
    return _second.Transform(_first.Transform(input));
  }

}