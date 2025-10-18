namespace Interactions.Transformation;

internal sealed class ManySelector<T1, T2> : Transformer<IEnumerable<T1>, IEnumerable<T2>> {

  private readonly Func<T1, IEnumerable<T2>> _selector;

  internal ManySelector(Func<T1, IEnumerable<T2>> selector) {
    _selector = selector;
  }

  protected override IEnumerable<T2> TransformCore(IEnumerable<T1> input) {
    return input.SelectMany(_selector);
  }

}