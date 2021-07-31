using EcsRx.Entities;

namespace Game.Events
{
    public class EntityCollisionEvent
    {
        public IEntity Other { get; private set; }
        public IEntity Self { get; private set; }
        public string Tag { get; private set; }

        public EntityCollisionEvent(string tag, IEntity other, IEntity self)
        {
            Other = other;
            Self = self;
            Tag = tag;
        }
    }
}