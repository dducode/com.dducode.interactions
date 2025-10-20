using AutoFixture;
using Interactions.Transformation;
using JetBrains.Annotations;

namespace Interactions.Tests.Transformation;

[TestSubject(typeof(Base64Transformer))]
public class Base64TransformerTest {

  [Fact]
  public void ToBase64Test() {
    var fixture = new Fixture();
    string expected = Convert.ToBase64String(fixture.Create<byte[]>());
    SymmetricTransformer<byte[], string> transformer = Transformer.Base64Transformer();
    byte[] data = transformer.InverseTransform(expected);

    Assert.Equal(expected, transformer.Transform(data));
  }

}