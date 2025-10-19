namespace Interactions.Transformation;

internal sealed class AnonymousTransformer<T1, T2> : Transformer<T1, T2> {

  private readonly Func<T1, T2> _filter;

  internal AnonymousTransformer(Func<T1, T2> filter) {
    _filter = filter;
  }

  protected override T2 TransformCore(T1 input) {
    return _filter(input);
  }

}