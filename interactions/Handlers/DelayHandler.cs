using Interactions.Core;

namespace Interactions.Handlers;

internal sealed class DelayHandler<T>(Func<T, TimeSpan> delay) : AsyncHandler<T, T> {

  public override async ValueTask<T> Handle(T input, CancellationToken token = default) {
    await Task.Delay(delay(input), token);
    return input;
  }

}