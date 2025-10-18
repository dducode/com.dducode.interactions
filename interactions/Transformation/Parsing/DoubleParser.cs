using System.Globalization;

namespace Interactions.Transformation.Parsing;

internal sealed class DoubleParser : Parser<double> {

  internal static DoubleParser Instance { get; } = new();
  private readonly CultureInfo _cultureInfo;

  internal DoubleParser(CultureInfo cultureInfo = null) {
    _cultureInfo = cultureInfo ?? CultureInfo.CurrentCulture;
  }

  protected override double Parse(string input) {
    return double.Parse(input, _cultureInfo);
  }

}