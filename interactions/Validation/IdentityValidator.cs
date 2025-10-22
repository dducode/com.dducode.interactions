namespace Interactions.Validation;

internal sealed class IdentityValidator<T> : Validator<T> {

  internal static IdentityValidator<T> Instance { get; } = new();
  public override string ErrorMessage => string.Empty;

  private IdentityValidator() {
  }

  protected internal override bool IsValid(T value) {
    return true;
  }

}