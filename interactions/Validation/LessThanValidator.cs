namespace Interactions.Validation;

internal sealed class LessThanValidator<T>(T compared, IComparer<T> comparer = null) : Validator<T> {

  private readonly IComparer<T> _comparer = comparer ?? Comparer<T>.Default;

  public override string ErrorMessage { get; } = $"Value must be less than {compared}";

  public override bool IsValid(T value) {
    return _comparer.Compare(value, compared) < 0;
  }

}