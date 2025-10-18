namespace Interactions.Validation;

internal sealed class AllValidator<T> : Validator<IEnumerable<T>> {

  private readonly Validator<T> _itemValidator;

  internal AllValidator(Validator<T> itemValidator) {
    _itemValidator = itemValidator;
    ErrorMessage = $"All elements must satisfy: {_itemValidator.ErrorMessage}";
  }

  public override string ErrorMessage { get; }

  protected override bool IsValidCore(IEnumerable<T> value) {
    return value.All(_itemValidator.IsValid);
  }

}