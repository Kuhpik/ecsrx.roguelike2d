using SystemsRx.Attributes;
using SystemsRx.Systems.Conventional;
using SystemsRx.Types;
using EcsRx.Collections.Database;
using EcsRx.Extensions;
using EcsRx.Unity.Extensions;
using Game.Components;
using Game.Events;

namespace Game.Systems
{
    [Priority(PriorityTypes.Low)]
    public class CoinPickupSystem : IReactToEventSystem<CoinPickupEvent>
    {
        private readonly IEntityDatabase _entityDatabase;

        public CoinPickupSystem(IEntityDatabase entityDatabase)
        { _entityDatabase = entityDatabase; }

        public void Process(CoinPickupEvent eventData)
        {
            var playerComponent = eventData.Player.GetComponent<PlayerComponent>();
            var coinsComponent = eventData.Coin.GetComponent<CoinComponent>();

            playerComponent.Coins.Value += coinsComponent.Count;

            this.AfterUpdateDo(x => { _entityDatabase.RemoveEntity(eventData.Coin); });
        }
    }
}