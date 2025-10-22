using Interactions.Core.Queries;
using Interactions.Extensions;
using Interactions.Transformation;
using Interactions.Transformation.Filtering;
using Interactions.Transformation.Parsing;
using JetBrains.Annotations;
using static Interactions.Validation.Validator;

namespace Interactions.Tests.Transformation;

[TestSubject(typeof(TransformHandler<,,,>))]
public class TransformHandlerTest {

  [Fact]
  public void ParseNumberTest() {
    var query = new Query<string, string>();
    using IDisposable handle = query.Handle(Handler.FromMethod<int>(num => num + num).Parse(Parser.Integer()));

    Assert.Equal("84", query.Send("42"));
    Assert.Throws<FormatException>(() => query.Send("not-a-number"));
    Assert.Throws<OverflowException>(() => query.Send("1251328907421983752137032985702938"));
  }

  [Fact]
  public void FilterListTest() {
    var query = new Query<IEnumerable<string>, string>();
    using IDisposable handle = query.Handle(Handler
      .Identity<string>()
      .InputTransform(Transformer.First<string>())
      .InputFilter(Filter.Where(StringLength(MoreThan(2))))
    );

    Assert.Equal("input", query.Send(new List<string> { string.Empty, "10", "input" }));
  }

}