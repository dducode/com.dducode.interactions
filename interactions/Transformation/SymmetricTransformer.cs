namespace Interactions.Transformation;

public abstract class SymmetricTransformer<T1, T2> : Transformer<T1, T2> {

  public abstract T1 InverseTransform(T2 input);

}