namespace Interactions.Validation;

internal sealed class StringLengthValidator(Validator<int> lengthValidator) : Validator<string> {

  public override string ErrorMessage { get; } = lengthValidator.ErrorMessage;

  protected override bool IsValidCore(string value) {
    return lengthValidator.IsValid(value.Length);
  }

}