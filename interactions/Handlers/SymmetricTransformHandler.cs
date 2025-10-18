using Interactions.Transformation;

namespace Interactions.Handlers;

internal sealed class SymmetricTransformHandler<T1, T2> : Handler<T1, T1> {

  private readonly SymmetricTransformer<T1, T2> _transformer;
  private readonly Handler<T2, T2> _handler;

  internal SymmetricTransformHandler(SymmetricTransformer<T1, T2> transformer, Handler<T2, T2> handler) {
    _transformer = transformer;
    _handler = handler;
  }

  protected override T1 HandleCore(T1 input) {
    return _transformer.InverseTransform(_handler.Handle(_transformer.Transform(input)));
  }

}

internal sealed class AsyncSymmetricTransformHandler<T1, T2> : AsyncHandler<T1, T1> {

  private readonly SymmetricTransformer<T1, T2> _transformer;
  private readonly AsyncHandler<T2, T2> _handler;

  internal AsyncSymmetricTransformHandler(SymmetricTransformer<T1, T2> transformer, AsyncHandler<T2, T2> handler) {
    _transformer = transformer;
    _handler = handler;
  }

  protected override async ValueTask<T1> HandleCore(T1 input, CancellationToken token = default) {
    return _transformer.InverseTransform(await _handler.Handle(_transformer.Transform(input), token));
  }

}