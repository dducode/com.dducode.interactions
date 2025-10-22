namespace Interactions.Validation;

internal sealed class AnyValidator<T>(Validator<T> itemValidator) : Validator<IEnumerable<T>> {

  public override string ErrorMessage { get; } = $"At least one element must satisfy: {itemValidator.ErrorMessage}";

  protected internal override bool IsValid(IEnumerable<T> value) {
    return value.Any(itemValidator.IsValid);
  }

}