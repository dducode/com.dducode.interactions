using System.Globalization;

namespace Interactions.Transformation.Parsing;

internal sealed class TimeParser(CultureInfo cultureInfo = null) : Parser<TimeSpan> {

  internal static TimeParser Instance { get; } = new();
  private readonly CultureInfo _cultureInfo = cultureInfo ?? CultureInfo.CurrentCulture;

  protected override TimeSpan Parse(string input) {
    return TimeSpan.Parse(input, _cultureInfo);
  }

}