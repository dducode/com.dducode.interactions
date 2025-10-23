using Interactions.Actions;
using Interactions.Core;

namespace Interactions.Handlers;

internal sealed class FinallyHandler<T1, T2>(Handler<T1, T2> handler, Finally<T1> @finally) : Handler<T1, T2> {

  protected internal override T2 Handle(T1 input) {
    try {
      return handler.Handle(input);
    }
    finally {
      @finally(input);
    }
  }

}