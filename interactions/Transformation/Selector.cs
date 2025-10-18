namespace Interactions.Transformation;

internal sealed class Selector<T1, T2> : Transformer<IEnumerable<T1>, IEnumerable<T2>> {

  private readonly Func<T1, T2> _selection;

  internal Selector(Func<T1, T2> selection) {
    _selection = selection;
  }

  protected override IEnumerable<T2> TransformCore(IEnumerable<T1> input) {
    return input.Select(_selection);
  }

}