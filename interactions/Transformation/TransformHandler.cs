using Interactions.Core.Handlers;

namespace Interactions.Transformation;

internal sealed class TransformHandler<T1, T2, T3, T4>(
  Transformer<T1, T2> inputTransformer,
  Handler<T2, T3> handler,
  Transformer<T3, T4> outputTransformer) : Handler<T1, T4> {

  protected internal override T4 Handle(T1 input) {
    return outputTransformer.Transform(handler.Handle(inputTransformer.Transform(input)));
  }

}

internal sealed class AsyncTransformHandler<T1, T2, T3, T4>(
  Transformer<T1, T2> inputTransformer,
  AsyncHandler<T2, T3> handler,
  Transformer<T3, T4> outputTransformer) : AsyncHandler<T1, T4> {

  protected internal override async ValueTask<T4> Handle(T1 input, CancellationToken token = default) {
    return outputTransformer.Transform(await handler.Handle(inputTransformer.Transform(input), token));
  }

}