using Interactions.Core;

namespace Interactions.Handlers;

internal sealed class CompositeHandler<T1, T2, T3>(Handler<T1, T2> first, Handler<T2, T3> second) : Handler<T1, T3> {

  protected internal override T3 Handle(T1 input) {
    return second.Handle(first.Handle(input));
  }

}