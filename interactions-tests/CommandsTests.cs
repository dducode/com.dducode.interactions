using Interactions.Commands;
using Xunit.Abstractions;

namespace Interactions.Tests;

public class CommandsTests(ITestOutputHelper testOutputHelper) {

  [Fact]
  public void ReversibleCommandTest() {
    var player = new HandlersTests.Player {
      data = new HandlersTests.PlayerData {
        money = 0
      }
    };

    var filterDecorator = new InputIntParseDecorator();

    var undoCommand = new Command<string>();
    undoCommand.Handle(filterDecorator.Decorate(Handler.FromCommandMethod<int>(num => player.data.money -= num)));

    var command = new ReversibleCommand<string>(undoCommand);
    command.Handle(filterDecorator.Decorate(Handler.FromCommandMethod<int>(num => player.data.money += num)));

    testOutputHelper.WriteLine("Start execute");

    for (var i = 0; i < 3; i++) {
      command.Execute("10");
      testOutputHelper.WriteLine($"Player money: {player.data.money}");
    }

    for (var i = 1; i <= 3; i++) {
      testOutputHelper.WriteLine($"Start undo: {i}");
      while (command.Undo())
        testOutputHelper.WriteLine($"Player money: {player.data.money}");

      testOutputHelper.WriteLine($"Start redo: {i}");
      while (command.Redo())
        testOutputHelper.WriteLine($"Player money: {player.data.money}");
    }
  }

  private class InputIntParseDecorator : Decorator<Handler<int, bool>, Handler<string, bool>> {

    public override Handler<string, bool> Decorate(Handler<int, bool> handler) {
      return handler.InputTransform<string>(int.Parse);
    }

  }

}