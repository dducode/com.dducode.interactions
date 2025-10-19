namespace Interactions.Validation;

internal sealed class LessThanValidator<T>(T value, IComparer<T> comparer = null) : Validator<T> {

  private readonly IComparer<T> _comparer = comparer ?? Comparer<T>.Default;

  public override string ErrorMessage { get; } = $"Value must be less than {value}";

  protected override bool IsValidCore(T value1) {
    return _comparer.Compare(value1, value) < 0;
  }

}