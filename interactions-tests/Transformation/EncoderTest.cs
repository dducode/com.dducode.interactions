using AutoFixture;
using Interactions.Transformation;
using JetBrains.Annotations;

namespace Interactions.Tests.Transformation;

[TestSubject(typeof(Encoder))]
public class EncoderTest {

  [Fact]
  public void EncodeDataTest() {
    var fixture = new Fixture();
    var expected = fixture.Create<string>();
    SymmetricTransformer<byte[], string> transformer = Transformer.Encode();
    byte[] data = transformer.InverseTransform(expected);

    Assert.Equal(expected, transformer.Transform(data));
  }

}