using System.Diagnostics.Contracts;
using Interactions.Validation;

namespace Interactions.Extensions;

public static class ValidatorExtensions {

  [Pure]
  public static Validator<T> And<T>(this Validator<T> first, Validator<T> second) {
    return new AndValidator<T>(first, second);
  }

  [Pure]
  public static Validator<T> Or<T>(this Validator<T> first, Validator<T> second) {
    return new OrValidator<T>(first, second);
  }

  [Pure]
  public static Validator<T> Inverse<T>(this Validator<T> validator) {
    return new NotValidator<T>(validator);
  }

  [Pure]
  public static Validator<T> OverrideMessage<T>(this Validator<T> validator, string message) {
    return new OverrideMessageValidator<T>(validator, message);
  }

}