namespace Interactions.Validation;

internal sealed class NotValidator<T>(Validator<T> inner) : Validator<T> {

  public override string ErrorMessage { get; } = $"NOT ({inner.ErrorMessage})";

  protected override bool IsValidCore(T value) {
    return !inner.IsValid(value);
  }

}