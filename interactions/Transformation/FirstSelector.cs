namespace Interactions.Transformation;

internal sealed class FirstSelector<T> : Transformer<IEnumerable<T>, T> {

  internal static FirstSelector<T> Instance { get; } = new();
  private readonly Func<T, bool> _predicate;

  internal FirstSelector(Func<T, bool> predicate = null) {
    _predicate = predicate ?? (_ => true);
  }

  protected override T TransformCore(IEnumerable<T> input) {
    return input.First(_predicate);
  }

}