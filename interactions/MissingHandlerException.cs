namespace Interactions;

public class MissingHandlerException : InvalidOperationException {

  internal MissingHandlerException(string message) : base(message) {
  }

}