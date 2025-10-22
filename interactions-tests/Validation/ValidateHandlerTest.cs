using Interactions.Core.Queries;
using Interactions.Extensions;
using Interactions.Validation;
using JetBrains.Annotations;

namespace Interactions.Tests.Validation;

[TestSubject(typeof(ValidateHandler<,>))]
public class ValidateHandlerTest {

  [Fact]
  public void ValidateRequestTest() {
    var query = new Query<string, string>();
    using IDisposable handle = query.Handle(Handler
      .FromMethod<string>(request => $"response: {request}")
      .ValidateInput(Validator.NotEmptyString)
    );

    Assert.Throws<InvalidInputException>(() => query.Send(string.Empty));
    Assert.Equal("response: request", query.Send("request"));
  }

}