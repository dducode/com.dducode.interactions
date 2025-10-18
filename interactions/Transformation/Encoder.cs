using System.Text;

namespace Interactions.Transformation;

internal sealed class Encoder : SymmetricTransformer<byte[], string> {

  internal static Encoder FromUTF8 { get; } = new(Encoding.UTF8);
  private readonly Encoding _encoding;

  internal Encoder(Encoding encoding) {
    _encoding = encoding;
  }

  protected override string TransformCore(byte[] input) {
    return _encoding.GetString(input);
  }

  protected override byte[] InverseTransformCore(string input) {
    return _encoding.GetBytes(input);
  }

}