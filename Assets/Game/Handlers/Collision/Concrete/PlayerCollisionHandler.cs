using EcsRx.Entities;
using Game.Events;
using Game.Services;
using SystemsRx.Events;

namespace Game.Handlers.Collision
{
    public class PlayerCollisionHandler : CollisionHander
    {
        public override string Tag => "Player";

        public PlayerCollisionHandler(IEventSystem eventSystem, EntityCollisionListener collisionListener)
            : base(eventSystem, collisionListener) { }

        public override void OnCollision(IEntity player, IEntity enemy)
        {
            _eventSystem.Publish(new PlayerHitEvent(player, enemy));
        }
    }
}