namespace Interactions.Transformation;

internal sealed class ListTransformer<T> : SymmetricTransformer<IEnumerable<T>, List<T>> {

  internal static ListTransformer<T> Instance { get; } = new();

  private ListTransformer() {
  }

  protected override List<T> TransformCore(IEnumerable<T> input) {
    return input.ToList();
  }

  protected override IEnumerable<T> InverseTransformCore(List<T> input) {
    return input.AsEnumerable();
  }

}