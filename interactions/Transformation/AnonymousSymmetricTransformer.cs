namespace Interactions.Transformation;

internal sealed class AnonymousSymmetricTransformer<T1, T2>(Func<T1, T2> forward, Func<T2, T1> backward) : SymmetricTransformer<T1, T2> {

  protected override T2 TransformCore(T1 input) {
    return forward(input);
  }

  protected override T1 InverseTransformCore(T2 input) {
    return backward(input);
  }

}