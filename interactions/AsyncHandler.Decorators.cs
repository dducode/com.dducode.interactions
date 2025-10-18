using System.Diagnostics.Contracts;
using Interactions.Handlers;
using Interactions.Transformation;

namespace Interactions;

public abstract partial class AsyncHandler<TIn, TOut> {

  [Pure]
  public AsyncHandler<TIn, TOut> Catch<TException>(AsyncCatch<TException, TIn, TOut> @catch) where TException : Exception {
    return new AsyncCatchHandler<TException, TIn, TOut>(this, @catch);
  }

  [Pure]
  public AsyncHandler<TIn, TOut> Catch<TException>(Catch<TException, TIn, TOut> @catch) where TException : Exception {
    return new AsyncCatchHandler<TException, TIn, TOut>(this, (exception, input) => new ValueTask<TOut>(@catch(exception, input)));
  }

  [Pure]
  public AsyncHandler<TIn, TOut> Finally(AsyncFinally<TIn> @finally) {
    return new AsyncFinallyHandler<TIn, TOut>(this, @finally);
  }

  [Pure]
  public AsyncHandler<TIn, TOut> Finally(Finally<TIn> @finally) {
    return new AsyncFinallyHandler<TIn, TOut>(this, input => {
      @finally(input);
      return new ValueTask();
    });
  }

  [Pure]
  public AsyncHandler<TTIn, TTOut> Transform<TTIn, TTOut>(Transformer<TTIn, TIn> incoming,
    Transformer<TOut, TTOut> outgoing) {
    return new AsyncTransformHandler<TTIn, TIn, TOut, TTOut>(incoming, this, outgoing);
  }

  [Pure]
  public AsyncHandler<TTin, TTOut> Transform<TTin, TTOut>(Func<TTin, TIn> incoming, Func<TOut, TTOut> outgoing) {
    return Transform(Transformer.FromMethod(incoming), Transformer.FromMethod(outgoing));
  }

  [Pure]
  public AsyncHandler<TTIn, TOut> InputTransform<TTIn>(Transformer<TTIn, TIn> incoming) {
    return Transform(incoming, Transformer.Identity<TOut>());
  }

  [Pure]
  public AsyncHandler<TIn, TTOut> OutputTransform<TTOut>(Transformer<TOut, TTOut> outgoing) {
    return Transform(Transformer.Identity<TIn>(), outgoing);
  }

  [Pure]
  public AsyncHandler<TTIn, TOut> InputTransform<TTIn>(Func<TTIn, TIn> incoming) {
    return InputTransform(Transformer.FromMethod(incoming));
  }

  [Pure]
  public AsyncHandler<TIn, TTOut> OutputTransform<TTOut>(Func<TOut, TTOut> outgoing) {
    return OutputTransform(Transformer.FromMethod(outgoing));
  }

}