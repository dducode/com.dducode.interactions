namespace Interactions.Transformation;

internal sealed class Selector<T1, T2>(Func<T1, T2> selection) : Transformer<IEnumerable<T1>, IEnumerable<T2>> {

  protected internal override IEnumerable<T2> Transform(IEnumerable<T1> input) {
    return input.Select(selection);
  }

}