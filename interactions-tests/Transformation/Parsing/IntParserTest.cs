using AutoFixture;
using Interactions.Transformation.Parsing;
using JetBrains.Annotations;

namespace Interactions.Tests.Transformation.Parsing;

[TestSubject(typeof(IntParser))]
public class IntParserTest {

  [Fact]
  public void ParseIntegerTest() {
    var fixture = new Fixture();
    Parser<int> parser = Parser.Integer();
    var expectedNum = fixture.Create<int>();

    Assert.Equal(expectedNum, parser.Transform(expectedNum.ToString()));
    Assert.NotEqual(expectedNum, parser.Transform((expectedNum + fixture.Create<int>()).ToString()));
  }

}