namespace Interactions.Transformation.Filtering;

internal sealed class Skipper<T>(int skipCount) : Filter<T> {

  protected override IEnumerable<T> ApplyCore(IEnumerable<T> input) {
    return input.Skip(skipCount);
  }

}