using Interactions.Extensions;
using static Interactions.Validation.Validator;

namespace Interactions.Validation;

internal sealed class CollectionCountValidator<T>(Validator<int> countValidator) : Validator<ICollection<T>> {

  internal static Validator<ICollection<T>> NotEmptyCollection { get; } = NotNull<ICollection<T>>().And(CollectionCount<T>(MoreThan(0)))
    .OverrideMessage($"Collection {typeof(ICollection<T>).Name} cannot be null or empty");

  public override string ErrorMessage { get; } = countValidator.ErrorMessage;

  protected internal override bool IsValid(ICollection<T> value) {
    return countValidator.IsValid(value.Count);
  }

}