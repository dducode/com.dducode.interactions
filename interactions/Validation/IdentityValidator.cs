namespace Interactions.Validation;

internal sealed class IdentityValidator<T> : Validator<T> {

  internal static IdentityValidator<T> Instance { get; } = new();
  public override string ErrorMessage => string.Empty;

  protected override bool IsValidCore(T value) {
    return true;
  }

}