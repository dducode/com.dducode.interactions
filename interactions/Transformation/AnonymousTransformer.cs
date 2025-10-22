namespace Interactions.Transformation;

internal sealed class AnonymousTransformer<T1, T2>(Func<T1, T2> transformation) : Transformer<T1, T2> {

  protected internal override T2 Transform(T1 input) {
    return transformation(input);
  }

}