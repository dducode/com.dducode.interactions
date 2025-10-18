namespace Interactions.Validation;

internal sealed class AnonymousValidator<T> : Validator<T> {

  public override string ErrorMessage { get; }

  private readonly Func<T, bool> _validation;

  internal AnonymousValidator(Func<T, bool> validation, string errorMessage) {
    _validation = validation;
    ErrorMessage = errorMessage;
  }

  protected override bool IsValidCore(T value) {
    return _validation(value);
  }

}