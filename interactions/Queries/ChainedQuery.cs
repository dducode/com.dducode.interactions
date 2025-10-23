using Interactions.Core;
using Interactions.Core.Queries;

namespace Interactions.Queries;

internal sealed class ChainedQuery<T1, T2, T3>(Query<T1, T2> first, Query<T2, T3> second) : Query<T1, T3> {

  public override T3 Send(T1 input) {
    return second.Send(first.Send(input));
  }

  public override IDisposable Handle(Handler<T1, T3> handler) {
    throw new InvalidOperationException("Cannot handle chained request");
  }

}