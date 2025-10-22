using AutoFixture;
using Interactions.Core.Queries;
using Interactions.Extensions;
using Interactions.Handlers;
using JetBrains.Annotations;

namespace Interactions.Tests.Handlers;

[TestSubject(typeof(ChainedHandler<,,>))]
public class ChainedHandlerTest {

  [Fact]
  public void GetPlayerMoneyFromStorageTest() {
    var fixture = new Fixture();
    var firstPlayerMoney = fixture.Create<decimal>();
    var secondPlayerMoney = fixture.Create<decimal>();

    var storage = new PlayerStorage();
    storage.Add(new Player {
      id = 0,
      data = new PlayerData {
        money = firstPlayerMoney
      }
    });
    storage.Add(new Player {
      id = 1,
      data = new PlayerData {
        money = secondPlayerMoney
      }
    });
    var fallbackPlayer = new Player();

    var query = new Query<int, decimal>();
    using IDisposable handle = query.Handle(Handler
      .FromMethod<int, Player>(id => storage.Get(id))
      .Catch((KeyNotFoundException _, int _) => fallbackPlayer)
      .Next(player => player.data)
      .Next(data => data.money)
    );

    Assert.Equal(firstPlayerMoney, query.Send(0));
    Assert.Equal(secondPlayerMoney, query.Send(1));
    Assert.Equal(0, query.Send(2));
  }

}

file class PlayerStorage {

  private readonly Dictionary<int, Player> _storage = new();

  public void Add(Player player) {
    _storage.Add(player.id, player);
  }

  public Player Get(int id) {
    return _storage[id];
  }

}

file class Player {

  public int id;
  public PlayerData data;

}

file struct PlayerData {

  public decimal money;

}