namespace Interactions.Transformation.Filtering;

internal sealed class Skipper<T>(int skipCount) : Filter<T> {

  protected override IEnumerable<T> Apply(IEnumerable<T> input) {
    return input.Skip(skipCount);
  }

}