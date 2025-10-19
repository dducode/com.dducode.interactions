using System.Globalization;

namespace Interactions.Transformation.Parsing;

internal sealed class IntParser(CultureInfo cultureInfo = null) : Parser<int> {

  internal static IntParser Instance { get; } = new();
  private readonly CultureInfo _cultureInfo = cultureInfo ?? CultureInfo.CurrentCulture;

  protected override int Parse(string input) {
    return int.Parse(input, _cultureInfo);
  }

}