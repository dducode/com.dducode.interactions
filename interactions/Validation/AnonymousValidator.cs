namespace Interactions.Validation;

internal sealed class AnonymousValidator<T>(Func<T, bool> validation, string errorMessage) : Validator<T> {

  public override string ErrorMessage { get; } = errorMessage;

  protected internal override bool IsValid(T value) {
    return validation(value);
  }

}