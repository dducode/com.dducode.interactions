using Interactions.Core.Extensions;
using Interactions.Core.Queries;
using JetBrains.Annotations;

namespace Interactions.Core.Tests.Queries;

[TestSubject(typeof(AsyncQuery<,>))]
public class AsyncQueryTest {

  [Fact]
  public async Task Test() {
    var query = new AsyncQuery<int, string>();
    Assert.False((await query.TrySend(0)).IsSuccess);
    query.Handle(i => i.ToString());

    if ((await query.TrySend(42)).TryGetValue(out string value))
      Assert.Equal("42", value);
    else
      Assert.Fail();
  }

}