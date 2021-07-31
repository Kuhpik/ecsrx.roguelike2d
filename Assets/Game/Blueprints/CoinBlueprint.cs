using EcsRx.Blueprints;
using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.Plugins.Views.Components;
using Game.Components;
using UnityEngine;

namespace Game.Blueprints
{
    public class CoinBlueprint : IBlueprint
    {
        CoinComponent GenerateCoin()
        {
            var count = Random.Range(1, 4);
            return new CoinComponent(count);
        }

        public void Apply(IEntity entity)
        {
            entity.AddComponents
            (
                GenerateCoin(), new ViewComponent(),
                new RandomlyPlacedComponent()
            );
        }
    }
}