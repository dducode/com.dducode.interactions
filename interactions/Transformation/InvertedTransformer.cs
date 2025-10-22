namespace Interactions.Transformation;

internal sealed class InvertedTransformer<T1, T2>(SymmetricTransformer<T1, T2> inner) : SymmetricTransformer<T2, T1> {

  protected internal override T1 Transform(T2 input) {
    return inner.InverseTransform(input);
  }

  protected internal override T2 InverseTransform(T1 input) {
    return inner.Transform(input);
  }

}