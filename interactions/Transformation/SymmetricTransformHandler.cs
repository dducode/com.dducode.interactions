namespace Interactions.Transformation;

internal sealed class SymmetricTransformHandler<T1, T2>(SymmetricTransformer<T1, T2> transformer, Handler<T2, T2> handler) : Handler<T1, T1> {

  protected override T1 HandleCore(T1 input) {
    return transformer.InverseTransform(handler.Handle(transformer.Transform(input)));
  }

}

internal sealed class AsyncSymmetricTransformHandler<T1, T2>(
  SymmetricTransformer<T1, T2> transformer,
  AsyncHandler<T2, T2> handler) : AsyncHandler<T1, T1> {

  protected override async ValueTask<T1> HandleCore(T1 input, CancellationToken token = default) {
    return transformer.InverseTransform(await handler.Handle(transformer.Transform(input), token));
  }

}