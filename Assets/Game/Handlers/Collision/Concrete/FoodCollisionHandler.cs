using EcsRx.Entities;
using EcsRx.Extensions;
using Game.Components;
using Game.Events;
using Game.Services;
using System.Collections.Generic;
using SystemsRx.Events;

namespace Game.Handlers.Collision
{
    public class FoodCollisionHandler : CollisionHander
    {
        protected override List<string> _tags => new List<string>() { "Food", "Soda" };

        public FoodCollisionHandler(IEventSystem eventSystem, EntityCollisionListener collisionListener)
            : base(eventSystem, collisionListener) { }

        public override void OnCollision(IEntity food, IEntity player)
        {
            _eventSystem.Publish(new FoodPickupEvent(food, player, food.GetComponent<FoodComponent>().IsSoda));
        }
    }
}