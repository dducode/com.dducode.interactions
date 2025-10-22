namespace Interactions.Validation;

internal sealed class OverrideMessageValidator<T>(Validator<T> inner, string errorMessage) : Validator<T> {

  public override string ErrorMessage { get; } = errorMessage;

  protected internal override bool IsValid(T value) {
    return inner.IsValid(value);
  }

}