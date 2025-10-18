using System.Diagnostics.Contracts;
using Interactions.Handlers;
using Interactions.Transformation;

namespace Interactions;

public abstract partial class Handler<TIn, TOut> {

  [Pure]
  public Handler<TIn, TOut> Catch<TException>(Catch<TException, TIn, TOut> @catch) where TException : Exception {
    return new CatchHandler<TException, TIn, TOut>(this, @catch);
  }

  [Pure]
  public Handler<TIn, TOut> Finally(Finally<TIn> @finally) {
    return new FinallyHandler<TIn, TOut>(this, @finally);
  }

  [Pure]
  public Handler<TTIn, TTOut> Transform<TTIn, TTOut>(Transformer<TTIn, TIn> incoming, Transformer<TOut, TTOut> outgoing) {
    return new TransformHandler<TTIn, TIn, TOut, TTOut>(incoming, this, outgoing);
  }

  [Pure]
  public Handler<TTIn, TTOut> Transform<TTIn, TTOut>(Func<TTIn, TIn> incoming, Func<TOut, TTOut> outgoing) {
    return Transform(Transformer.FromMethod(incoming), Transformer.FromMethod(outgoing));
  }

  [Pure]
  public Handler<TTIn, TOut> InputTransform<TTIn>(Transformer<TTIn, TIn> incoming) {
    return Transform(incoming, Transformer.Identity<TOut>());
  }

  [Pure]
  public Handler<TIn, TTOut> OutputTransform<TTOut>(Transformer<TOut, TTOut> outgoing) {
    return Transform(Transformer.Identity<TIn>(), outgoing);
  }

  [Pure]
  public Handler<TTIn, TOut> InputTransform<TTIn>(Func<TTIn, TIn> incoming) {
    return InputTransform(Transformer.FromMethod(incoming));
  }

  [Pure]
  public Handler<TIn, TTOut> OutputTransform<TTOut>(Func<TOut, TTOut> outgoing) {
    return OutputTransform(Transformer.FromMethod(outgoing));
  }

}