using Interactions.Transformation;

namespace Interactions.Handlers;

internal sealed class TransformHandler<T1, T2, T3, T4> : Handler<T1, T4> {

  private readonly Transformer<T1, T2> _inputTransformer;
  private readonly Handler<T2, T3> _handler;
  private readonly Transformer<T3, T4> _outputTransformer;

  internal TransformHandler(
    Transformer<T1, T2> inputTransformer,
    Handler<T2, T3> handler,
    Transformer<T3, T4> outputTransformer) {
    _inputTransformer = inputTransformer;
    _handler = handler;
    _outputTransformer = outputTransformer;
  }

  protected override T4 HandleCore(T1 input) {
    return _outputTransformer.Transform(_handler.Handle(_inputTransformer.Transform(input)));
  }

}

internal sealed class AsyncTransformHandler<T1, T2, T3, T4> : AsyncHandler<T1, T4> {

  private readonly Transformer<T1, T2> _inputTransformer;
  private readonly AsyncHandler<T2, T3> _handler;
  private readonly Transformer<T3, T4> _outputTransformer;

  internal AsyncTransformHandler(
    Transformer<T1, T2> inputTransformer,
    AsyncHandler<T2, T3> handler,
    Transformer<T3, T4> outputTransformer) {
    _inputTransformer = inputTransformer;
    _handler = handler;
    _outputTransformer = outputTransformer;
  }

  protected override async ValueTask<T4> HandleCore(T1 input, CancellationToken token = default) {
    return _outputTransformer.Transform(await _handler.Handle(_inputTransformer.Transform(input), token));
  }

}