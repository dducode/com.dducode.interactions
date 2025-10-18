namespace Interactions;

public interface IDeconstructable<T1, T2> {

  void Deconstruct(out T1 first, out T2 second);

}

public interface IDeconstructable<T1, T2, T3> {

  void Deconstruct(out T1 first, out T2 second, out T3 third);

}

public interface IDeconstructable<T1, T2, T3, T4> {

  void Deconstruct(out T1 first, out T2 second, out T3 third, out T4 fourth);

}