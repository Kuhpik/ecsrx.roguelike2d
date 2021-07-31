using EcsRx.Entities;
using Game.Events;
using Game.Services;
using SystemsRx.Events;

namespace Game.Handlers.Collision
{
    public class EnemyCollisionHandler : CollisionHander
    {
        public override string Tag => "Enemy";

        public EnemyCollisionHandler(IEventSystem eventSystem, EntityCollisionListener collisionListener)
            : base(eventSystem, collisionListener) { }

        public override void OnCollision(IEntity enemy, IEntity player)
        {
            _eventSystem.Publish(new EnemyHitEvent(enemy, player));
        }
    }
}