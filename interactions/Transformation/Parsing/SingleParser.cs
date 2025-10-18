using System.Globalization;

namespace Interactions.Transformation.Parsing;

internal sealed class SingleParser : Parser<float> {

  internal static SingleParser Instance { get; } = new();
  private readonly CultureInfo _cultureInfo;

  internal SingleParser(CultureInfo cultureInfo = null) {
    _cultureInfo = cultureInfo ?? CultureInfo.CurrentCulture;
  }

  protected override float Parse(string input) {
    return float.Parse(input, _cultureInfo);
  }

}