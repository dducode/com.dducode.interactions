namespace Interactions.Transformation;

internal sealed class ListTransformer<T> : SymmetricTransformer<IEnumerable<T>, List<T>> {

  internal static ListTransformer<T> Instance { get; } = new();

  private ListTransformer() {
  }

  protected internal override List<T> Transform(IEnumerable<T> input) {
    return input.ToList();
  }

  protected internal override IEnumerable<T> InverseTransform(List<T> input) {
    return input.AsEnumerable();
  }

}