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
    using IDisposable handle = query.Handle(Branch<Unit, int>
      .If(() => state == 0, _ => 0)
      .ElseIf(() => state == 1, _ => 1)
      .ElseIf(() => state == 2, _ => 2)
      .Else(_ => -1)
    );

    Assert.Equal(expected, query.Send());
  }

}