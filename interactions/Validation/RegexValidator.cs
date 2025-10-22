using System.Text.RegularExpressions;

namespace Interactions.Validation;

internal sealed class RegexValidator : Validator<string> {

  private readonly Regex _regex;

  internal RegexValidator(string pattern, RegexOptions options = RegexOptions.None) {
    _regex = new Regex(pattern, options);
    ErrorMessage = $"Value does not match pattern: {_regex}";
  }

  public override string ErrorMessage { get; }

  protected internal override bool IsValid(string value) {
    return _regex.IsMatch(value);
  }

}