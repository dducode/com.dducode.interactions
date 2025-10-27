namespace Interactions.Transformation.Parsing;

internal sealed class AnonymousParser<T>(Func<string, T> parsing) : Parser<T> {

  protected override T Parse(string input) {
    return parsing(input);
  }

}