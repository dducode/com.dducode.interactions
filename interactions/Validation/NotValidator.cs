namespace Interactions.Validation;

internal sealed class NotValidator<T> : Validator<T> {

  private readonly Validator<T> _inner;

  internal NotValidator(Validator<T> inner) {
    _inner = inner;
    ErrorMessage = $"NOT ({_inner.ErrorMessage})";
  }

  public override string ErrorMessage { get; }

  protected override bool IsValidCore(T value) {
    return !_inner.IsValid(value);
  }

}