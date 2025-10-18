namespace Interactions.Validation;

internal sealed class StringLengthValidator : Validator<string> {

  private readonly Validator<int> _lengthValidator;

  internal StringLengthValidator(Validator<int> lengthValidator) {
    _lengthValidator = lengthValidator;
    ErrorMessage = _lengthValidator.ErrorMessage;
  }

  public override string ErrorMessage { get; }

  protected override bool IsValidCore(string value) {
    return _lengthValidator.IsValid(value.Length);
  }

}