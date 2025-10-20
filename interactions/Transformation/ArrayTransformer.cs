namespace Interactions.Transformation;

internal sealed class ArrayTransformer<T> : SymmetricTransformer<IEnumerable<T>, T[]> {

  internal static ArrayTransformer<T> Instance { get; } = new();

  private ArrayTransformer() {
  }

  protected override T[] TransformCore(IEnumerable<T> input) {
    return input.ToArray();
  }

  protected override IEnumerable<T> InverseTransformCore(T[] input) {
    return input.AsEnumerable();
  }

}