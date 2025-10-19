using System.Globalization;

namespace Interactions.Transformation.Parsing;

internal sealed class SingleParser(CultureInfo cultureInfo = null) : Parser<float> {

  internal static SingleParser Instance { get; } = new();
  private readonly CultureInfo _cultureInfo = cultureInfo ?? CultureInfo.CurrentCulture;

  protected override float Parse(string input) {
    return float.Parse(input, _cultureInfo);
  }

}