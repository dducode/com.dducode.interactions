using Interactions.Core.Handlers;

namespace Interactions.Handlers;

internal sealed class ChainedHandler<T1, T2, T3>(Handler<T1, T2> first, Handler<T2, T3> next) : Handler<T1, T3> {

  protected internal override T3 Handle(T1 input) {
    return next.Handle(first.Handle(input));
  }

}