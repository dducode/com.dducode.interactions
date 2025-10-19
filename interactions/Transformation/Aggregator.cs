namespace Interactions.Transformation;

internal sealed class Aggregator<T>(Func<T, T, T> accumulate) : Transformer<IEnumerable<T>, T> {

  protected override T TransformCore(IEnumerable<T> input) {
    return input.Aggregate(accumulate);
  }

}

internal sealed class Aggregator<T1, T2>(Func<T2> seed, Func<T2, T1, T2> accumulate) : Transformer<IEnumerable<T1>, T2> {

  protected override T2 TransformCore(IEnumerable<T1> input) {
    return input.Aggregate(seed(), accumulate);
  }

}