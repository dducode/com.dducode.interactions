using System.Diagnostics.Contracts;
using System.Text;

namespace Interactions.Transformation;

public abstract class Transformer<T1, T2> {

  internal T2 Transform(T1 input) {
    return TransformCore(input);
  }

  protected abstract T2 TransformCore(T1 input);

}

public static class Transformer {

  [Pure]
  public static Transformer<T, T> Identity<T>() {
    return IdentityTransformer<T>.Instance;
  }

  [Pure]
  public static Transformer<byte[], string> Encode(Encoding encoding = null) {
    return encoding == null ? Encoder.FromUTF8 : new Encoder(encoding);
  }

  [Pure]
  public static Transformer<byte[], string> Base64Transformer() {
    return Transformation.Base64Transformer.Instance;
  }

  [Pure]
  public static Transformer<IEnumerable<T>, T> Aggregate<T>(Func<T, T, T> accumulate) {
    return new Aggregator<T>(accumulate);
  }

  [Pure]
  public static Transformer<IEnumerable<T1>, T2> Aggregate<T1, T2>(Func<T2> seed, Func<T2, T1, T2> accumulate) {
    return new Aggregator<T1, T2>(seed, accumulate);
  }

  [Pure]
  public static Transformer<IEnumerable<T1>, IEnumerable<T2>> Select<T1, T2>(Func<T1, T2> selection) {
    return new Selector<T1, T2>(selection);
  }

  [Pure]
  public static Transformer<IEnumerable<T1>, IEnumerable<T2>> SelectMany<T1, T2>(Func<T1, IEnumerable<T2>> selection) {
    return new ManySelector<T1, T2>(selection);
  }

  [Pure]
  public static Transformer<IEnumerable<T>, T> First<T>(Func<T, bool> predicate = null) {
    return predicate == null ? FirstSelector<T>.Instance : new FirstSelector<T>(predicate);
  }

  [Pure]
  public static Transformer<T1, T2> FromMethod<T1, T2>(Func<T1, T2> transformation) {
    return new AnonymousTransformer<T1, T2>(transformation);
  }

  [Pure]
  public static Transformer<T1, T2> FromMethod<T1, T2>(Func<T1, T2> forward, Func<T2, T1> backward) {
    return new AnonymousSymmetricTransformer<T1, T2>(forward, backward);
  }

}