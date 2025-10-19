namespace Interactions;

public class InvalidInputException : InvalidOperationException {

  internal InvalidInputException(string message) : base(message) {
  }

}

public class InvalidOutputException : InvalidOperationException {

  internal InvalidOutputException(string message) : base(message) {
  }

}