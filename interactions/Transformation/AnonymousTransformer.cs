namespace Interactions.Transformation;

internal sealed class AnonymousTransformer<TIn, TOut> : Transformer<TIn, TOut> {

  private readonly Func<TIn, TOut> _filter;

  internal AnonymousTransformer(Func<TIn, TOut> filter) {
    _filter = filter;
  }

  protected override TOut TransformCore(TIn input) {
    return _filter(input);
  }

}