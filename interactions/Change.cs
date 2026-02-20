namespace Interactions;

public readonly record struct Change<T>(T Old, T New) {

  public readonly T Old = Old;
  public readonly T New = New;

}