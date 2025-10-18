namespace Interactions.Transformation;

internal sealed class Aggregator<T> : Transformer<IEnumerable<T>, T> {

  private readonly Func<T, T, T> _accumulate;

  internal Aggregator(Func<T, T, T> accumulate) {
    _accumulate = accumulate;
  }

  protected override T TransformCore(IEnumerable<T> input) {
    return input.Aggregate(_accumulate);
  }

}

internal sealed class Aggregator<T1, T2> : Transformer<IEnumerable<T1>, T2> {

  private readonly Func<T2> _seed;
  private readonly Func<T2, T1, T2> _accumulate;

  internal Aggregator(Func<T2> seed, Func<T2, T1, T2> accumulate) {
    _seed = seed;
    _accumulate = accumulate;
  }

  protected override T2 TransformCore(IEnumerable<T1> input) {
    return input.Aggregate(_seed(), _accumulate);
  }

}