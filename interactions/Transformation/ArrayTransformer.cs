namespace Interactions.Transformation;

internal sealed class ArrayTransformer<T> : SymmetricTransformer<IEnumerable<T>, T[]> {

  internal static ArrayTransformer<T> Instance { get; } = new();

  private ArrayTransformer() {
  }

  protected internal override T[] Transform(IEnumerable<T> input) {
    return input.ToArray();
  }

  protected internal override IEnumerable<T> InverseTransform(T[] input) {
    return input.AsEnumerable();
  }

}