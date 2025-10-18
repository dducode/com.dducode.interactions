namespace Interactions.Validation;

internal sealed class MoreThanValidator<T> : Validator<T> {

  private readonly T _value;
  private readonly IComparer<T> _comparer;

  internal MoreThanValidator(T value, IComparer<T> comparer = null) {
    _value = value;
    _comparer = comparer ?? Comparer<T>.Default;
    ErrorMessage = $"Value must be more than {_value}";
  }

  public override string ErrorMessage { get; }

  protected override bool IsValidCore(T value) {
    return _comparer.Compare(value, _value) > 0;
  }

}