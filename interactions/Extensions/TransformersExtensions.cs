using System.Diagnostics.Contracts;
using Interactions.Transformation;

namespace Interactions.Extensions;

public static class TransformersExtensions {

  [Pure]
  public static SymmetricTransformer<T2, T1> Inverse<T1, T2>(this SymmetricTransformer<T1, T2> transformer) {
    return new InvertedTransformer<T1, T2>(transformer);
  }

}