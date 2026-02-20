namespace Interactions.Transformation;

internal sealed class Base64Transformer : SymmetricTransformer<byte[], string> {

  internal static Base64Transformer Instance { get; } = new();

  private Base64Transformer() {
  }

  public override string Transform(byte[] input) {
    return Convert.ToBase64String(input);
  }

  public override byte[] InverseTransform(string input) {
    return Convert.FromBase64String(input);
  }

}