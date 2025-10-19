namespace Interactions.Validation;

internal sealed class MoreThanValidator<T>(T value, IComparer<T> comparer = null) : Validator<T> {

  private readonly IComparer<T> _comparer = comparer ?? Comparer<T>.Default;

  public override string ErrorMessage { get; } = $"Value must be more than {value}";

  protected override bool IsValidCore(T value1) {
    return _comparer.Compare(value1, value) > 0;
  }

}