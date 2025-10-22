namespace Interactions.Validation;

internal sealed class AndValidator<T>(Validator<T> first, Validator<T> second) : Validator<T> {

  public override string ErrorMessage { get; } = $"{first.ErrorMessage} or {second.ErrorMessage}";

  protected internal override bool IsValid(T value) {
    return first.IsValid(value) && second.IsValid(value);
  }

}