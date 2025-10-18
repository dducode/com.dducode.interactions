namespace Interactions.Validation;

internal sealed class OverrideMessageValidator<T> : Validator<T> {

  private readonly Validator<T> _inner;

  internal OverrideMessageValidator(Validator<T> inner, string errorMessage) {
    _inner = inner;
    ErrorMessage = errorMessage;
  }

  public override string ErrorMessage { get; }

  protected override bool IsValidCore(T value) {
    return _inner.IsValid(value);
  }

}