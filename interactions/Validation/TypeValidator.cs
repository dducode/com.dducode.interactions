namespace Interactions.Validation;

internal sealed class TypeValidator<TExpected> : Validator<object> {

  internal static TypeValidator<TExpected> Instance { get; } = new();
  public override string ErrorMessage { get; } = $"Value must be of type {typeof(TExpected).Name}";

  private TypeValidator() {
  }

  protected internal override bool IsValid(object value) {
    return value is TExpected;
  }

}