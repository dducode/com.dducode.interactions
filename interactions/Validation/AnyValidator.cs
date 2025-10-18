namespace Interactions.Validation;

internal sealed class AnyValidator<T> : Validator<IEnumerable<T>> {

  private readonly Validator<T> _itemValidator;

  internal AnyValidator(Validator<T> itemValidator) {
    _itemValidator = itemValidator;
    ErrorMessage = $"At least one element must satisfy: {_itemValidator.ErrorMessage}";
  }

  public override string ErrorMessage { get; }

  protected override bool IsValidCore(IEnumerable<T> value) {
    return value.Any(_itemValidator.IsValid);
  }

}