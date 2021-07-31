using System;
using System.Collections.Generic;
using SystemsRx.Events;
using SystemsRx.Extensions;
using SystemsRx.Systems.Conventional;
using EcsRx.Entities;
using EcsRx.Groups;
using EcsRx.Groups.Observable;
using EcsRx.Plugins.GroupBinding.Attributes;
using EcsRx.Systems;
using EcsRx.Unity.Extensions;
using EcsRx.Unity.MonoBehaviours;
using EcsRx.Plugins.Views.Components;
using Game.Components;
using Game.Events;
using UniRx;
using UniRx.Triggers;

namespace Game.Systems
{
    public class PlayerInteractionSystem : IManualSystem, IGroupSystem
    {
        public IGroup Group { get; } = new Group(typeof (PlayerComponent), typeof (ViewComponent));

        [FromGroup]
        public IObservableGroup ObservableGroup;
        
        private readonly IList<IDisposable> _interactionTriggers = new List<IDisposable>();
        private readonly IEventSystem _eventSystem;

        public PlayerInteractionSystem(IEventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }

        public void StartSystem()
        {
            this.WaitForScene().Subscribe(x =>
            {
                foreach(var player in ObservableGroup)
                { CheckForInteractions(player); }
            });
        }

        public void StopSystem()
        {
            _interactionTriggers.DisposeAll();
        }

        private void CheckForInteractions(IEntity player)
        {
            var currentPlayer = player;
            var playerView = currentPlayer.GetGameObject();
            var triggerObservable = playerView.OnTriggerEnter2DAsObservable();

            var triggers = triggerObservable.Subscribe(x =>
            {
                var tag = x.gameObject.tag;
                var entityView = x.gameObject.GetComponent<EntityView>();

                _eventSystem.Publish(new EntityCollisionEvent(tag, entityView.Entity, currentPlayer));
            });

            _interactionTriggers.Add(triggers);
        }
    }
}