namespace Interactions.Transformation;

internal sealed class AnonymousSymmetricTransformer<T1, T2> : SymmetricTransformer<T1, T2> {

  private readonly Func<T1, T2> _forward;
  private readonly Func<T2, T1> _backward;

  internal AnonymousSymmetricTransformer(Func<T1, T2> forward, Func<T2, T1> backward) {
    _forward = forward;
    _backward = backward;
  }

  protected override T2 TransformCore(T1 input) {
    return _forward(input);
  }

  protected override T1 InverseTransformCore(T2 input) {
    return _backward(input);
  }

}