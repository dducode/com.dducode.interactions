namespace Interactions.Transformation.Filtering;

internal sealed class UniqueFilter<T>(IEqualityComparer<T> comparer = null) : Filter<T> {

  internal static UniqueFilter<T> Instance { get; } = new();
  private readonly IEqualityComparer<T> _comparer = comparer ?? EqualityComparer<T>.Default;

  protected override IEnumerable<T> ApplyCore(IEnumerable<T> input) {
    return input.Distinct(_comparer);
  }

}