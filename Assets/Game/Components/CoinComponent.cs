using EcsRx.Components;

namespace Game.Components
{
    public class CoinComponent : IComponent
    {
        public readonly int Count;

        public CoinComponent(int count)
        {
            Count = count;
        }
    }
}