using Interactions.Validation;

namespace Interactions.Transformation.Filtering;

internal sealed class ConditionalFilter<T>(Validator<T> itemValidator) : Filter<T> {

  protected override IEnumerable<T> ApplyCore(IEnumerable<T> input) {
    return input.Where(itemValidator.IsValid);
  }

}