namespace Interactions.Transformation;

internal sealed class Base64Transformer : SymmetricTransformer<byte[], string> {

  internal static Base64Transformer Instance { get; } = new();

  private Base64Transformer() {
  }

  protected override string TransformCore(byte[] input) {
    return Convert.ToBase64String(input);
  }

  protected override byte[] InverseTransformCore(string input) {
    return Convert.FromBase64String(input);
  }

}