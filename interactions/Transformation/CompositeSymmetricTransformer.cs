namespace Interactions.Transformation;

internal sealed class CompositeSymmetricTransformer<T1, T2, T3>(
  SymmetricTransformer<T1, T2> first,
  SymmetricTransformer<T2, T3> second) : SymmetricTransformer<T1, T3> {

  public override T3 Transform(T1 input) {
    return second.Transform(first.Transform(input));
  }

  public override T1 InverseTransform(T3 input) {
    return first.InverseTransform(second.InverseTransform(input));
  }

}