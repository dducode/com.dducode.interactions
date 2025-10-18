using System.Globalization;

namespace Interactions.Transformation.Parsing;

internal sealed class IntParser : Parser<int> {

  internal static IntParser Instance { get; } = new();
  private readonly CultureInfo _cultureInfo;

  internal IntParser(CultureInfo cultureInfo = null) {
    _cultureInfo = cultureInfo ?? CultureInfo.CurrentCulture;
  }

  protected override int Parse(string input) {
    return int.Parse(input, _cultureInfo);
  }

}