namespace Interactions.Transformation;

internal sealed class ArrayTransformer<T> : SymmetricTransformer<IEnumerable<T>, T[]> {

  internal static ArrayTransformer<T> Instance { get; } = new();

  private ArrayTransformer() {
  }

  public override T[] Transform(IEnumerable<T> input) {
    return input.ToArray();
  }

  public override IEnumerable<T> InverseTransform(T[] input) {
    return input;
  }

}