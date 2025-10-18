namespace Interactions.Validation;

internal sealed class AndValidator<T> : Validator<T> {

  private readonly Validator<T> _first;
  private readonly Validator<T> _second;

  internal AndValidator(Validator<T> first, Validator<T> second) {
    _first = first;
    _second = second;
    ErrorMessage = $"{_first.ErrorMessage} or {_second.ErrorMessage}";
  }

  public override string ErrorMessage { get; }

  protected override bool IsValidCore(T value) {
    return _first.IsValid(value) && _second.IsValid(value);
  }

}