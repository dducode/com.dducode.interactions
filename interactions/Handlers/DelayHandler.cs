using Interactions.Core.Handlers;

namespace Interactions.Handlers;

internal sealed class DelayHandler<T>(Func<T, TimeSpan> delay) : AsyncHandler<T, T> {

  protected internal override async ValueTask<T> Handle(T input, CancellationToken token = default) {
    await Task.Delay(delay(input), token);
    return input;
  }

}