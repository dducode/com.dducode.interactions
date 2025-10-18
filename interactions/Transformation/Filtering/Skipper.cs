namespace Interactions.Transformation.Filtering;

internal sealed class Skipper<T> : Filter<T> {

  private readonly int _skipCount;

  internal Skipper(int skipCount) {
    _skipCount = skipCount;
  }

  protected override IEnumerable<T> ApplyCore(IEnumerable<T> input) {
    return input.Skip(_skipCount);
  }

}