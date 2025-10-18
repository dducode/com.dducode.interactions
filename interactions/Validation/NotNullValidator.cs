namespace Interactions.Validation;

internal sealed class NotNullValidator<T> : Validator<T> {

  internal static NotNullValidator<T> Instance { get; } = new();
  public override string ErrorMessage => "Value cannot be null";

  protected override bool IsValidCore(T value) {
    return value != null;
  }

}