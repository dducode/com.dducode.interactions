namespace Interactions.Transformation;

public abstract class SymmetricTransformer<T1, T2> : Transformer<T1, T2> {

  internal T1 InverseTransform(T2 input) {
    return InverseTransformCore(input);
  }

  protected abstract T1 InverseTransformCore(T2 input);

}