namespace Interactions.Validation;

internal sealed class OrValidator<T> : Validator<T> {

  private readonly Validator<T> _first;
  private readonly Validator<T> _second;

  internal OrValidator(Validator<T> first, Validator<T> second) {
    _first = first;
    _second = second;
    ErrorMessage = $"{_first.ErrorMessage} and {_second.ErrorMessage}";
  }

  public override string ErrorMessage { get; }

  protected override bool IsValidCore(T value) {
    return _first.IsValid(value) || _second.IsValid(value);
  }

}