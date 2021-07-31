using EcsRx.Entities;
using Game.Events;
using Game.Handlers.Collision;
using System;
using System.Collections.Generic;
using SystemsRx.Events;
using UniRx;

namespace Game.Services
{
    public class EntityCollisionListener
    {
        readonly Dictionary<string, CollisionHander> _collisionHandlers;
        readonly IEventSystem _eventSystem;

        public EntityCollisionListener(IEventSystem eventSystem)
        {
            _eventSystem = eventSystem;
            _collisionHandlers = new Dictionary<string, CollisionHander>();

            _eventSystem.Receive<EntityCollisionEvent>().AsObservable().Subscribe(OnCollision);
        }

        void OnCollision(EntityCollisionEvent eventData)
        {
            if (_collisionHandlers.ContainsKey(eventData.Tag))
            {
                _collisionHandlers[eventData.Tag].OnCollision(eventData.Other, eventData.Self);
            }
        }

        public void Subscribe(CollisionHander handler)
        {
            foreach (var tag in handler.Tags)
            {
                if (!_collisionHandlers.ContainsKey(tag))
                {
                    _collisionHandlers.Add(tag, handler);
                }
            }            
        }
    }
}