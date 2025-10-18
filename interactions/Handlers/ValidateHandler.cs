using Interactions.Validation;

namespace Interactions.Handlers;

internal sealed class ValidateHandler<TIn, TOut> : Handler<TIn, TOut> {

  private readonly Validator<TIn> _inputValidator;
  private readonly Handler<TIn, TOut> _handler;
  private readonly Validator<TOut> _outputValidator;

  internal ValidateHandler(Validator<TIn> inputValidator, Handler<TIn, TOut> handler, Validator<TOut> outputValidator) {
    _inputValidator = inputValidator;
    _handler = handler;
    _outputValidator = outputValidator;
  }

  protected override TOut HandleCore(TIn input) {
    if (!_inputValidator.IsValid(input))
      throw new InvalidInputException(_inputValidator.ErrorMessage);
    TOut output = _handler.Handle(input);

    if (!_outputValidator.IsValid(output))
      throw new InvalidOutputException(_outputValidator.ErrorMessage);
    return output;
  }

}

internal sealed class AsyncValidateHandler<TIn, TOut> : AsyncHandler<TIn, TOut> {

  private readonly Validator<TIn> _inputValidator;
  private readonly AsyncHandler<TIn, TOut> _handler;
  private readonly Validator<TOut> _outputValidator;

  internal AsyncValidateHandler(Validator<TIn> inputValidator, AsyncHandler<TIn, TOut> handler, Validator<TOut> outputValidator) {
    _inputValidator = inputValidator;
    _handler = handler;
    _outputValidator = outputValidator;
  }

  protected override async ValueTask<TOut> HandleCore(TIn input, CancellationToken token = default) {
    if (!_inputValidator.IsValid(input))
      throw new InvalidInputException(_inputValidator.ErrorMessage);
    TOut output = await _handler.Handle(input, token);

    if (!_outputValidator.IsValid(output))
      throw new InvalidOutputException(_outputValidator.ErrorMessage);
    return output;
  }

}