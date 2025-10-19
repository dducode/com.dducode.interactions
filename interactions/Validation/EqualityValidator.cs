namespace Interactions.Validation;

internal sealed class EqualityValidator<T>(T expected, IEqualityComparer<T> equalityComparer = null) : Validator<T> {

  private readonly IEqualityComparer<T> _equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;

  public override string ErrorMessage { get; } = $"Value must be equal to {expected}";

  protected override bool IsValidCore(T value) {
    return _equalityComparer.Equals(expected, value);
  }

}