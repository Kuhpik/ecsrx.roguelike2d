using EcsRx.Collections.Database;
using EcsRx.Entities;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Plugins.Views.Components;
using EcsRx.Unity.Dependencies;
using EcsRx.Unity.Systems;
using Game.Components;
using Game.SceneCollections;
using System.Linq;
using SystemsRx.Attributes;
using SystemsRx.Events;
using UnityEngine;

namespace Game.ViewResolvers
{
    [Priority(100)]
    public class CoinViewResolver : DynamicViewResolverSystem
    {
        readonly CoinTiles _coinCollections;

        public override IGroup Group => new Group(typeof(CoinComponent), typeof(ViewComponent));

        public CoinViewResolver(IEventSystem eventSystem, IEntityDatabase entityDatabase, IUnityInstantiator instantiator, CoinTiles coinCollections)
            : base(eventSystem, entityDatabase, instantiator)
        {
            _coinCollections = coinCollections;
        }

        public override GameObject CreateView(IEntity entity)
        {
            var component = entity.GetComponent<CoinComponent>();
            var tile = _coinCollections.AvailableTiles.FirstOrDefault();
            var gameObject = Object.Instantiate(tile, Vector3.zero, Quaternion.identity);
            gameObject.name = $"Coin-{component.Count}";
            return gameObject;
        }

        public override void DestroyView(IEntity entity, GameObject view)
        {
            GameObject.Destroy(view);
        }
    }
}