namespace Interactions.Transformation;

internal sealed class ListTransformer<T> : SymmetricTransformer<IEnumerable<T>, List<T>> {

  internal static ListTransformer<T> Instance { get; } = new();

  private ListTransformer() {
  }

  public override List<T> Transform(IEnumerable<T> input) {
    return input.ToList();
  }

  public override IEnumerable<T> InverseTransform(List<T> input) {
    return input;
  }

}