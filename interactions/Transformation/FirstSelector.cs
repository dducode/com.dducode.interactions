namespace Interactions.Transformation;

internal sealed class FirstSelector<T>(Func<T, bool> predicate = null) : Transformer<IEnumerable<T>, T> {

  internal static FirstSelector<T> Instance { get; } = new();
  private readonly Func<T, bool> _predicate = predicate ?? (_ => true);

  public override T Transform(IEnumerable<T> input) {
    return input.First(_predicate);
  }

}