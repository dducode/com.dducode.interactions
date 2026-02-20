namespace Interactions.Transformation;

internal sealed class InvertedTransformer<T1, T2>(SymmetricTransformer<T1, T2> inner) : SymmetricTransformer<T2, T1> {

  public override T1 Transform(T2 input) {
    return inner.InverseTransform(input);
  }

  public override T2 InverseTransform(T1 input) {
    return inner.Transform(input);
  }

}