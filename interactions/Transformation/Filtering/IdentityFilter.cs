namespace Interactions.Transformation.Filtering;

internal sealed class IdentityFilter<T> : Filter<T> {

  internal static IdentityFilter<T> Instance { get; } = new();

  protected override IEnumerable<T> ApplyCore(IEnumerable<T> input) {
    return input;
  }

}