using Interactions.Extensions;
using Interactions.Queries;
using Interactions.Transformation;
using Interactions.Transformation.Parsing;
using JetBrains.Annotations;

namespace Interactions.Tests.Transformation;

[TestSubject(typeof(TransformHandler<,,,>))]
public class TransformHandlerTest {

  [Fact]
  public void ParseNumberTest() {
    var query = new Query<string, string>();
    using IDisposable handle = query.Handle(Handler.FromMethod<int, int>(num => num + num).Parse(Parser.Integer()));

    Assert.Equal("84", query.Send("42"));
    Assert.Throws<FormatException>(() => query.Send("not-a-number"));
    Assert.Throws<OverflowException>(() => query.Send("1251328907421983752137032985702938"));
  }

}