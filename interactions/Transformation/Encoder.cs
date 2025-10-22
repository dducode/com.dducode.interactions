using System.Text;

namespace Interactions.Transformation;

internal sealed class Encoder(Encoding encoding) : SymmetricTransformer<byte[], string> {

  internal static Encoder FromUTF8 { get; } = new(Encoding.UTF8);

  protected internal override string Transform(byte[] input) {
    return encoding.GetString(input);
  }

  protected internal override byte[] InverseTransform(string input) {
    return encoding.GetBytes(input);
  }

}