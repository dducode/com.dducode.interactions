using System.Globalization;

namespace Interactions.Transformation.Parsing;

public abstract class Parser<T> : Transformer<string, T> {

  protected internal override T Transform(string input) {
    return Parse(input);
  }

  protected abstract T Parse(string input);

}

public static class Parser {

  public static Parser<int> Integer(CultureInfo cultureInfo = null) {
    return cultureInfo == null ? IntParser.Instance : new IntParser(cultureInfo);
  }

  public static Parser<double> Double(CultureInfo cultureInfo = null) {
    return cultureInfo == null ? DoubleParser.Instance : new DoubleParser(cultureInfo);
  }

  public static Parser<float> Single(CultureInfo cultureInfo = null) {
    return cultureInfo == null ? SingleParser.Instance : new SingleParser(cultureInfo);
  }

  public static Parser<TimeSpan> TimeSpan(CultureInfo cultureInfo = null) {
    return cultureInfo == null ? TimeParser.Instance : new TimeParser(cultureInfo);
  }

}