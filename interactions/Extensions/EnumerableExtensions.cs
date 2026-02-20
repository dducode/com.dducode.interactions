using Interactions.Core;

namespace Interactions.Extensions;

public static class EnumerableExtensions {

  public static async ValueTask<TAccumulate> AggregateAsync<TAccumulate, TSource>(
    this IEnumerable<TSource> enumerable,
    TAccumulate seed,
    AsyncFunc<TAccumulate, TSource, TAccumulate> accumulate,
    CancellationToken token = default) {
    TAccumulate result = seed;

    foreach (TSource source in enumerable) {
      token.ThrowIfCancellationRequested();
      result = await accumulate(result, source, token);
    }

    return result;
  }

}