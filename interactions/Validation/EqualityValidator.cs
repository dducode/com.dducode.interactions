namespace Interactions.Validation;

internal sealed class EqualityValidator<T> : Validator<T> {

  private readonly T _expected;
  private readonly IEqualityComparer<T> _equalityComparer;

  internal EqualityValidator(T expected, IEqualityComparer<T> equalityComparer = null) {
    _expected = expected;
    _equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
    ErrorMessage = $"Value must be equal to {_expected}";
  }

  public override string ErrorMessage { get; }

  protected override bool IsValidCore(T value) {
    return _equalityComparer.Equals(_expected, value);
  }

}