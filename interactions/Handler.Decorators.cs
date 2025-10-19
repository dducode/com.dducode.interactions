using System.Diagnostics.Contracts;
using Interactions.Handlers;
using Interactions.Transformation;

namespace Interactions;

public abstract partial class Handler<T1, T2> {

  [Pure]
  public Handler<T1, T2> Catch<TException>(Catch<TException, T1, T2> @catch) where TException : Exception {
    return new CatchHandler<TException, T1, T2>(this, @catch);
  }

  [Pure]
  public Handler<T1, T2> Finally(Finally<T1> @finally) {
    return new FinallyHandler<T1, T2>(this, @finally);
  }

  [Pure]
  public Handler<K1, K2> Transform<K1, K2>(Transformer<K1, T1> incoming, Transformer<T2, K2> outgoing) {
    return new TransformHandler<K1, T1, T2, K2>(incoming, this, outgoing);
  }

  [Pure]
  public Handler<K1, K2> Transform<K1, K2>(Func<K1, T1> incoming, Func<T2, K2> outgoing) {
    return Transform(Transformer.FromMethod(incoming), Transformer.FromMethod(outgoing));
  }

  [Pure]
  public Handler<K1, T2> InputTransform<K1>(Transformer<K1, T1> incoming) {
    return Transform(incoming, Transformer.Identity<T2>());
  }

  [Pure]
  public Handler<T1, K2> OutputTransform<K2>(Transformer<T2, K2> outgoing) {
    return Transform(Transformer.Identity<T1>(), outgoing);
  }

  [Pure]
  public Handler<K1, T2> InputTransform<K1>(Func<K1, T1> incoming) {
    return InputTransform(Transformer.FromMethod(incoming));
  }

  [Pure]
  public Handler<T1, K2> OutputTransform<K2>(Func<T2, K2> outgoing) {
    return OutputTransform(Transformer.FromMethod(outgoing));
  }

}