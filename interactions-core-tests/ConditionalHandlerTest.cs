using Interactions.Core.Extensions;
using Interactions.Core.Handlers;
using Interactions.Core.Queries;
using JetBrains.Annotations;

namespace Interactions.Core.Tests;

[TestSubject(typeof(ConditionalHandler<,>))]
public class ConditionalHandlerTest {

  [Theory]
  [InlineData(0, 0)]
  [InlineData(1, 1)]
  [InlineData(2, 2)]
  [InlineData(3, -1)]
  public void GetDataDependingOnExternalStateTest(int state, int expected) {
    var query = new Query<Unit, int>();
    using IDisposable handle = query.Handle(Branch<int>
      .If(() => state == 0, () => 0)
      .ElseIf(() => state == 1, () => 1)
      .ElseIf(() => state == 2, () => 2)
      .Else(() => -1)
    );

    Assert.Equal(expected, query.Send());
  }

  [Theory]
  [InlineData(true, true, 0)]
  [InlineData(true, false, 1)]
  [InlineData(false, true, 2)]
  [InlineData(false, false, 3)]
  public void NestedBranchTest(bool topConditional, bool nestedConditional, int expected) {
    var query = new Query<Unit, int>();
    using IDisposable handle = query.Handle(Branch<int>
      .If(() => topConditional, Branch<int>
        .If(() => nestedConditional, () => 0)
        .Else(() => 1))
      .Else(Branch<int>
        .If(() => nestedConditional, () => 2)
        .Else(() => 3))
    );

    Assert.Equal(expected, query.Send());
  }

}