using System.Text;

namespace Interactions.Transformation;

internal sealed class Encoder(Encoding encoding) : SymmetricTransformer<byte[], string> {

  internal static Encoder FromUTF8 { get; } = new(Encoding.UTF8);

  protected override string TransformCore(byte[] input) {
    return encoding.GetString(input);
  }

  protected override byte[] InverseTransformCore(string input) {
    return encoding.GetBytes(input);
  }

}