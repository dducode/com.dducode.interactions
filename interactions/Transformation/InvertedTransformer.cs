namespace Interactions.Transformation;

internal sealed class InvertedTransformer<T1, T2>(SymmetricTransformer<T1, T2> inner) : SymmetricTransformer<T2, T1> {

  protected override T1 TransformCore(T2 input) {
    return inner.InverseTransform(input);
  }

  protected override T2 InverseTransformCore(T1 input) {
    return inner.Transform(input);
  }

}