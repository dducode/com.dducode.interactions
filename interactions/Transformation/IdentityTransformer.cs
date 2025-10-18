namespace Interactions.Transformation;

internal sealed class IdentityTransformer<T> : Transformer<T, T> {

  internal static readonly IdentityTransformer<T> Instance = new();

  protected override T TransformCore(T input) {
    return input;
  }

}