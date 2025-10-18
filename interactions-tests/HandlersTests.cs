using System.Globalization;
using Interactions.Extensions;
using Interactions.Queries;
using Interactions.Transformation.Parsing;
using Interactions.Validation;
using Xunit.Abstractions;

namespace Interactions.Tests;

public class HandlersTests(ITestOutputHelper testOutputHelper) {

  [Fact]
  public void ChainedHandlersTest() {
    var storage = new PlayerStorage();
    storage.Add(new Player {
      id = 0,
      data = new PlayerData {
        money = 100.99m
      }
    });
    storage.Add(new Player {
      id = 1,
      data = new PlayerData {
        money = 10000.50m
      }
    });

    Handler<int, decimal> getMoneyHandler = Handler
      .FromMethod<int, Player>(id => storage.Get(id))
      .Next(player => player.data)
      .Next(data => data.money);

    var query = new Query<int, decimal>();
    using IDisposable handle = query.Handle(getMoneyHandler);

    testOutputHelper.WriteLine(query.Send(0).ToString(CultureInfo.InvariantCulture));
    testOutputHelper.WriteLine(query.Send(1).ToString(CultureInfo.InvariantCulture));
  }

  [Fact]
  public void ConditionalHandlerTest() {
    var level = 0;

    Handler<string, string> branchedHandler = Handler<string, string>
      .If(() => level == 0, input => $"{input}: level == 0")
      .ElseIf(() => level == 1, input => $"{input}: level == 1")
      .Else(input => $"{input}: unknown level");

    var query = new Query<string, string>();
    using IDisposable handle = query.Handle(branchedHandler);

    testOutputHelper.WriteLine(query.Send("Step 1"));
    level = 1;
    testOutputHelper.WriteLine(query.Send("Step 2"));
    level = 2;
    testOutputHelper.WriteLine(query.Send("Step 3"));
  }

  [Fact]
  public void FilterHandlerTest() {
    Handler<string, string> filterHandler = Handler.FromMethod<int, int>(num => num + num).Parse(Parser.Integer());
    var query = new Query<string, string>();
    using IDisposable handle = query.Handle(filterHandler);
    testOutputHelper.WriteLine(query.Send("42"));
  }

  [Fact]
  public void UseHandlerTest() {
    Handler<Unit, Unit> useHandler = Handler<Unit, Unit>
      .Use((input, next) => {
        testOutputHelper.WriteLine("Start first");
        next.Invoke(input);
        testOutputHelper.WriteLine("End first");
        return input;
      })
      .Use((input, next) => {
        testOutputHelper.WriteLine("Start second");
        next.Invoke(input);
        testOutputHelper.WriteLine("End second");
        return input;
      })
      .End(input => {
        testOutputHelper.WriteLine("Finish");
        return input;
      });

    var query = new Query<Unit, Unit>();
    using IDisposable handle = query.Handle(useHandler);
    testOutputHelper.WriteLine(query.Send().ToString());
  }

  [Fact]
  public void ValidateHandlerTest() {
    Handler<string, string> validateHandler = Handler
      .FromMethod<string, string>(input => $"output from: {input}")
      .ValidateInput(Validator.StringLength(Validator.LessThanOrEqual(10)).OverrideMessage("Too long input"))
      .Catch<InvalidInputException>((exception, input) => $"{exception.Message}: {input}");

    var query = new Query<string, string>();
    using IDisposable handle = query.Handle(validateHandler);

    testOutputHelper.WriteLine(query.Send("input"));
    testOutputHelper.WriteLine(query.Send("000000000000000000000000000000000000000000000000000000000000000"));
  }

  public class PlayerStorage {

    private readonly Dictionary<int, Player> _storage = new();

    public void Add(Player player) {
      _storage.Add(player.id, player);
    }

    public Player Get(int id) {
      return _storage[id];
    }

    public Task<Player> GetAsync(int id) {
      return Task.FromResult(_storage[id]);
    }

  }

  public class Player {

    public int id;
    public PlayerData data;

  }

  public struct PlayerData {

    public decimal money;

  }

}