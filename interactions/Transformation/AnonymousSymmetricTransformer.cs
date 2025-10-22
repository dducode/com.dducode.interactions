namespace Interactions.Transformation;

internal sealed class AnonymousSymmetricTransformer<T1, T2>(Func<T1, T2> forward, Func<T2, T1> backward) : SymmetricTransformer<T1, T2> {

  protected internal override T2 Transform(T1 input) {
    return forward(input);
  }

  protected internal override T1 InverseTransform(T2 input) {
    return backward(input);
  }

}