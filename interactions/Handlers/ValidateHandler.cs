using Interactions.Validation;

namespace Interactions.Handlers;

internal sealed class ValidateHandler<T1, T2> : Handler<T1, T2> {

  private readonly Validator<T1> _inputValidator;
  private readonly Handler<T1, T2> _handler;
  private readonly Validator<T2> _outputValidator;

  internal ValidateHandler(Validator<T1> inputValidator, Handler<T1, T2> handler, Validator<T2> outputValidator) {
    _inputValidator = inputValidator;
    _handler = handler;
    _outputValidator = outputValidator;
  }

  protected override T2 HandleCore(T1 input) {
    if (!_inputValidator.IsValid(input))
      throw new InvalidInputException(_inputValidator.ErrorMessage);
    T2 output = _handler.Handle(input);

    if (!_outputValidator.IsValid(output))
      throw new InvalidOutputException(_outputValidator.ErrorMessage);
    return output;
  }

}

internal sealed class AsyncValidateHandler<T1, T2> : AsyncHandler<T1, T2> {

  private readonly Validator<T1> _inputValidator;
  private readonly AsyncHandler<T1, T2> _handler;
  private readonly Validator<T2> _outputValidator;

  internal AsyncValidateHandler(Validator<T1> inputValidator, AsyncHandler<T1, T2> handler, Validator<T2> outputValidator) {
    _inputValidator = inputValidator;
    _handler = handler;
    _outputValidator = outputValidator;
  }

  protected override async ValueTask<T2> HandleCore(T1 input, CancellationToken token = default) {
    if (!_inputValidator.IsValid(input))
      throw new InvalidInputException(_inputValidator.ErrorMessage);
    T2 output = await _handler.Handle(input, token);

    if (!_outputValidator.IsValid(output))
      throw new InvalidOutputException(_outputValidator.ErrorMessage);
    return output;
  }

}