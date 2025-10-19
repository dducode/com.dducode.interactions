namespace Interactions.Validation;

internal sealed class AllValidator<T>(Validator<T> itemValidator) : Validator<IEnumerable<T>> {

  public override string ErrorMessage { get; } = $"All elements must satisfy: {itemValidator.ErrorMessage}";

  protected override bool IsValidCore(IEnumerable<T> value) {
    return value.All(itemValidator.IsValid);
  }

}