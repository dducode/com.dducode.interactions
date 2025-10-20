using System.Diagnostics.Contracts;
using Interactions.Transformation;
using Interactions.Transformation.Filtering;

namespace Interactions.Extensions;

public static class CollectionsTransformExtensions {

  [Pure]
  public static Handler<List<T>, List<T>> ListFilter<T>(this Handler<List<T>, List<T>> handler, Filter<T> incoming, Filter<T> outgoing) {
    return handler.Transform(Transformer.ToList<T>())
      .Filter(incoming, outgoing)
      .Transform(Transformer.ToList<T>().Inverse());
  }

  [Pure]
  public static Handler<List<T1>, T2> InputListFilter<T1, T2>(this Handler<List<T1>, T2> handler, Filter<T1> incoming) {
    return handler.InputTransform(Transformer.ToList<T1>())
      .InputFilter(incoming)
      .InputTransform(Transformer.ToList<T1>().Inverse());
  }

  [Pure]
  public static Handler<T1, List<T2>> OutputListFilter<T1, T2>(this Handler<T1, List<T2>> handler, Filter<T2> outgoing) {
    return handler.OutputTransform(Transformer.ToList<T2>().Inverse())
      .OutputFilter(outgoing)
      .OutputTransform(Transformer.ToList<T2>());
  }

  [Pure]
  public static Handler<T[], T[]> ArrayFilter<T>(this Handler<T[], T[]> handler, Filter<T> incoming, Filter<T> outgoing) {
    return handler.Transform(Transformer.ToArray<T>())
      .Filter(incoming, outgoing)
      .Transform(Transformer.ToArray<T>().Inverse());
  }

  [Pure]
  public static Handler<T1[], T2> InputArrayFilter<T1, T2>(this Handler<T1[], T2> handler, Filter<T1> incoming) {
    return handler.InputTransform(Transformer.ToArray<T1>())
      .InputFilter(incoming)
      .InputTransform(Transformer.ToArray<T1>().Inverse());
  }

  [Pure]
  public static Handler<T1, T2[]> OutputArrayFilter<T1, T2>(this Handler<T1, T2[]> handler, Filter<T2> outgoing) {
    return handler.OutputTransform(Transformer.ToArray<T2>().Inverse())
      .OutputFilter(outgoing)
      .OutputTransform(Transformer.ToArray<T2>());
  }

}