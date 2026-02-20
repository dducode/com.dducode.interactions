namespace Interactions.Transformation;

internal sealed class CompositeTransformer<T1, T2, T3>(Transformer<T1, T2> first, Transformer<T2, T3> second) : Transformer<T1, T3> {

  public override T3 Transform(T1 input) {
    return second.Transform(first.Transform(input));
  }

}