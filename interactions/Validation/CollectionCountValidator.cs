using Interactions.Extensions;
using static Interactions.Validation.Validator;

namespace Interactions.Validation;

internal class CollectionCountValidator<T> : Validator<ICollection<T>> {

  internal static Validator<ICollection<T>> NotEmptyCollection { get; } = NotNull<ICollection<T>>().And(CollectionCount<T>(MoreThan(0)))
    .OverrideMessage($"Collection {typeof(ICollection<T>).Name} cannot be null or empty");

  private readonly Validator<int> _countValidator;

  internal CollectionCountValidator(Validator<int> countValidator) {
    _countValidator = countValidator;
    ErrorMessage = _countValidator.ErrorMessage;
  }

  public override string ErrorMessage { get; }

  protected override bool IsValidCore(ICollection<T> value) {
    return _countValidator.IsValid(value.Count);
  }

}