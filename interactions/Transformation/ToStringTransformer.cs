namespace Interactions.Transformation;

internal sealed class ToStringTransformer<T> : Transformer<T, string> {

  internal static ToStringTransformer<T> Instance { get; } = new();

  private ToStringTransformer() {
  }

  public override string Transform(T input) {
    return input?.ToString() ?? string.Empty;
  }

}