using AutoFixture;
using Interactions.Transformation;
using JetBrains.Annotations;

namespace Interactions.Tests.Transformation;

[TestSubject(typeof(Aggregator<>))]
public class AggregatorTest {

  [Fact]
  public void AddingNumbersTest() {
    var fixture = new Fixture();
    Transformer<IEnumerable<int>, int> transformer = Transformer.Aggregate<int>((first, second) => first + second);
    var list = new List<int>(fixture.CreateMany<int>(10));
    int expected = list.Sum();

    Assert.Equal(expected, transformer.Transform(list));
  }

}