using Interactions.Transformation;
using JetBrains.Annotations;

namespace Interactions.Tests.Transformation;

[TestSubject(typeof(SplitConcatStringsTransformer))]
public class SplitConcatStringsTransformerTest {

  [Theory]
  [InlineData(new[] { "test", "test", "test" }, "test, test, test")]
  public void SplitTest(string[] expected, string input) {
    Transformer<string, IEnumerable<string>> transformer = Transformer.Split(", ");
    Assert.Equal(expected, transformer.Transform(input));
  }

  [Theory]
  [InlineData("test, test, test", new[] { "test", "test", "test" })]
  public void ConcatTest(string expected, string[] input) {
    Transformer<IEnumerable<string>, string> transformer = Transformer.Concat(", ");
    Assert.Equal(expected, transformer.Transform(input));
  }

  [Theory]
  [InlineData("test, test, test")]
  public void SplitConcatTest(string expected) {
    SymmetricTransformer<string, IEnumerable<string>> transformer = Transformer.SplitConcat(", ");
    Assert.Equal(expected, transformer.InverseTransform(transformer.Transform(expected)));
  }

  [Theory]
  [InlineData([new[] { "test", "test", "test" }])]
  public void ConcatSplitTest(string[] expected) {
    SymmetricTransformer<IEnumerable<string>, string> transformer = Transformer.ConcatSplit(", ");
    Assert.Equal(expected, transformer.InverseTransform(transformer.Transform(expected)));
  }

}