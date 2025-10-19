namespace Interactions.Transformation;

internal sealed class AnonymousTransformer<T1, T2>(Func<T1, T2> transformation) : Transformer<T1, T2> {

  protected override T2 TransformCore(T1 input) {
    return transformation(input);
  }

}