using System.Diagnostics.Contracts;
using Interactions.Core;
using Interactions.Validation;

namespace Interactions.Extensions;

public static partial class ValidationExtensions {

  [Pure]
  public static Handler<T1, T2> Validate<T1, T2>(
    this Handler<T1, T2> handler, Validator<T1> inputValidator, Validator<T2> outputValidator) {
    return new ValidateHandler<T1, T2>(inputValidator, handler, outputValidator);
  }

  [Pure]
  public static Handler<T, T> Validate<T>(this Handler<T, T> handler, Validator<T> validator) {
    return handler.Validate(validator, validator);
  }

  [Pure]
  public static Handler<T1, T2> ValidateInput<T1, T2>(this Handler<T1, T2> handler, Validator<T1> validator) {
    return handler.Validate(validator, Validator.Identity<T2>());
  }

  [Pure]
  public static Handler<T1, T2> ValidateOutput<T1, T2>(this Handler<T1, T2> handler, Validator<T2> validator) {
    return handler.Validate(Validator.Identity<T1>(), validator);
  }

  [Pure]
  public static Handler<T1, T2> ValidateInput<T1, T2>(this Handler<T1, T2> handler, Func<T1, bool> validator, string errorMessage) {
    return handler.ValidateInput(Validator.FromMethod(validator, errorMessage));
  }

  [Pure]
  public static Handler<T1, T2> ValidateOutput<T1, T2>(this Handler<T1, T2> handler, Func<T2, bool> validator, string errorMessage) {
    return handler.ValidateOutput(Validator.FromMethod(validator, errorMessage));
  }

}