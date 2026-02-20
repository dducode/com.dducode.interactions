namespace Interactions.Transformation;

internal sealed class IdentityTransformer<T> : Transformer<T, T> {

  internal static readonly IdentityTransformer<T> Instance = new();

  private IdentityTransformer() {
  }

  public override T Transform(T input) {
    return input;
  }

}