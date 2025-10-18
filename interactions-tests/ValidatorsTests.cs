using Interactions.Extensions;
using Interactions.Queries;
using Interactions.Validation;
using Xunit.Abstractions;

namespace Interactions.Tests;

public class ValidatorsTests(ITestOutputHelper testOutputHelper) {

  [Fact]
  public void RangeTest() {
    var query = new Query<int, string>();
    using IDisposable handle = query.Handle(Handler
      .FromMethod<int, string>(num => $"received number: {num}")
      .ValidateInput(Validator.InRange(0, 3))
      .Catch<InvalidInputException>((exception, input) => $"{exception.Message}: {input}")
    );

    for (int i = -2; i < 5; i++)
      testOutputHelper.WriteLine(query.Send(i));
  }

}