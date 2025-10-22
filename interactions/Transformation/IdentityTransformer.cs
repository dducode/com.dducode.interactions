namespace Interactions.Transformation;

internal sealed class IdentityTransformer<T> : Transformer<T, T> {

  internal static readonly IdentityTransformer<T> Instance = new();

  private IdentityTransformer() {
  }

  protected internal override T Transform(T input) {
    return input;
  }

}