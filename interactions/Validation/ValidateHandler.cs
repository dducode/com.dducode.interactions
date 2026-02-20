using Interactions.Core;

namespace Interactions.Validation;

internal sealed class ValidateHandler<T1, T2>(
  Validator<T1> inputValidator,
  Handler<T1, T2> handler,
  Validator<T2> outputValidator) : Handler<T1, T2> {

  public override T2 Handle(T1 input) {
    if (!inputValidator.IsValid(input))
      throw new InvalidInputException(inputValidator.ErrorMessage);
    T2 output = handler.Handle(input);

    if (!outputValidator.IsValid(output))
      throw new InvalidOutputException(outputValidator.ErrorMessage);
    return output;
  }

}

internal sealed class AsyncValidateHandler<T1, T2>(
  Validator<T1> inputValidator,
  AsyncHandler<T1, T2> handler,
  Validator<T2> outputValidator) : AsyncHandler<T1, T2> {

  public override async ValueTask<T2> Handle(T1 input, CancellationToken token = default) {
    if (!inputValidator.IsValid(input))
      throw new InvalidInputException(inputValidator.ErrorMessage);
    T2 output = await handler.Handle(input, token);

    if (!outputValidator.IsValid(output))
      throw new InvalidOutputException(outputValidator.ErrorMessage);
    return output;
  }

}