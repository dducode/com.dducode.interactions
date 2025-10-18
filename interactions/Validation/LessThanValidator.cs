namespace Interactions.Validation;

internal sealed class LessThanValidator<T> : Validator<T> {

  private readonly T _value;
  private readonly IComparer<T> _comparer;

  internal LessThanValidator(T value, IComparer<T> comparer = null) {
    _value = value;
    _comparer = comparer ?? Comparer<T>.Default;
    ErrorMessage = $"Value must be less than {_value}";
  }

  public override string ErrorMessage { get; }

  protected override bool IsValidCore(T value) {
    return _comparer.Compare(value, _value) < 0;
  }

}