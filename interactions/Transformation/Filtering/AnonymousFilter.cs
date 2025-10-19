namespace Interactions.Transformation.Filtering;

internal sealed class AnonymousFilter<T>(Func<IEnumerable<T>, IEnumerable<T>> filtration) : Filter<T> {

  protected override IEnumerable<T> ApplyCore(IEnumerable<T> input) {
    return filtration(input);
  }

}