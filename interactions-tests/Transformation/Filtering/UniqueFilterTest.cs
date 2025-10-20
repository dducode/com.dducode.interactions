using Interactions.Transformation.Filtering;
using JetBrains.Annotations;

namespace Interactions.Tests.Transformation.Filtering;

[TestSubject(typeof(UniqueFilter<>))]
public class UniqueFilterTest {

  [Fact]
  public void SelectUniqueNumbersTest() {
    var list = new List<int> { 1, 1, 2, 3, 3 };
    Filter<int> filter = Filter.Distinct<int>();

    Assert.Equal(3, filter.Transform(list).Count());
  }

}