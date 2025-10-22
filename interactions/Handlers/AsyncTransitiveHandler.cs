using Interactions.Actions;
using Interactions.Core.Handlers;

namespace Interactions.Handlers;

internal sealed class AsyncTransitiveHandler<T>(AsyncSideAction<T> action) : AsyncHandler<T, T> {

  protected internal override async ValueTask<T> Handle(T input, CancellationToken token = default) {
    await action(input, token);
    return input;
  }

}