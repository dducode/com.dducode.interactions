using Interactions.Actions;
using Interactions.Core;

namespace Interactions.Handlers;

internal sealed class CatchHandler<TException, T1, T2>(
  Handler<T1, T2> handler,
  Catch<TException, T1, T2> @catch) : Handler<T1, T2> where TException : Exception {

  protected internal override T2 Handle(T1 input) {
    try {
      return handler.Handle(input);
    }
    catch (TException e) {
      return @catch(e, input);
    }
  }

}