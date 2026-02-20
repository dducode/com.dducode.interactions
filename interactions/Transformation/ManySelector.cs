namespace Interactions.Transformation;

internal sealed class ManySelector<T1, T2>(Func<T1, IEnumerable<T2>> selector) : Transformer<IEnumerable<T1>, IEnumerable<T2>> {

  public override IEnumerable<T2> Transform(IEnumerable<T1> input) {
    return input.SelectMany(selector);
  }

}