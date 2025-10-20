using AutoFixture;
using Interactions.Transformation.Parsing;
using JetBrains.Annotations;

namespace Interactions.Tests.Transformation.Parsing;

[TestSubject(typeof(TimeParser))]
public class TimeParserTest {

  [Fact]
  public void ParseTimeSpanTest() {
    var fixture = new Fixture();
    Parser<TimeSpan> parser = Parser.TimeSpan();
    var expectedTime = fixture.Create<TimeSpan>();

    Assert.Equal(expectedTime, parser.Transform(expectedTime.ToString()));
    Assert.NotEqual(expectedTime, parser.Transform((expectedTime + fixture.Create<TimeSpan>()).ToString()));
  }

}