using Interactions.Extensions;
using Interactions.Handlers;
using Interactions.Queries;
using JetBrains.Annotations;
using Xunit.Abstractions;

namespace Interactions.Tests.Handlers;

[TestSubject(typeof(UseHandler<,>))]
public class UseHandlerTest(ITestOutputHelper testOutputHelper) {

  [Fact]
  public void NestedInvocationTest() {
    var firstStarted = false;
    var secondStarted = false;

    var secondEnd = false;
    var thirdEnd = false;

    var query = new Query<Unit, Unit>();
    using IDisposable handle = query.Handle(Handler<Unit, Unit>
      .Use((input, next) => {
        testOutputHelper.WriteLine("Start first");
        firstStarted = true;
        next.Invoke(input);
        Assert.True(secondEnd);
        testOutputHelper.WriteLine("End first");
        return input;
      })
      .Use((input, next) => {
        Assert.True(firstStarted);
        testOutputHelper.WriteLine("Start second");
        secondStarted = true;
        next.Invoke(input);
        Assert.True(thirdEnd);
        testOutputHelper.WriteLine("End second");
        secondEnd = true;
        return input;
      })
      .End(input => {
        Assert.True(secondStarted);
        testOutputHelper.WriteLine("Finish");
        thirdEnd = true;
        return input;
      })
    );

    query.Send();
  }

}