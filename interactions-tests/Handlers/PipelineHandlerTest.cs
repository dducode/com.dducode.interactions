using Interactions.Core;
using Interactions.Core.Extensions;
using Interactions.Core.Queries;
using Interactions.Extensions;
using Interactions.Handlers;
using JetBrains.Annotations;
using Xunit.Abstractions;

namespace Interactions.Tests.Handlers;

[TestSubject(typeof(PipelineHandler<,,,>))]
public class PipelineHandlerTest(ITestOutputHelper testOutputHelper) {

  [Fact]
  public void NestedInvocationTest() {
    var firstStarted = false;
    var secondStarted = false;

    var secondEnd = false;
    var thirdEnd = false;

    var query = new Query<Unit, Unit>();
    using IDisposable handle = query.Handle(Pipeline<Unit, Unit>
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

  [Theory]
  [InlineData("00:05:00", "00:05:05", 5)]
  [InlineData("00:00:10", "00:00:00", -10)]
  public void NestedInvocationWithDifferentTypesTest(string input, string expected, int addedSeconds) {
    var query = new Query<string, string>();
    using IDisposable handle = query.Handle(Pipeline<string, string>
      .Use<TimeSpan>((time, next) => next.Invoke(TimeSpan.Parse(time)).ToString())
      .Use<double>((timeSpan, next) => TimeSpan.FromSeconds(next.Invoke(timeSpan.TotalSeconds)))
      .End(seconds => seconds + addedSeconds)
    );

    Assert.Equal(expected, query.Send(input));
  }

}