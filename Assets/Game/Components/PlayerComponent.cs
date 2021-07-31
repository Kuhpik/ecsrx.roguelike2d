using System;
using EcsRx.Components;
using UniRx;

namespace Game.Components
{
    public class PlayerComponent : IComponent, IDisposable
    {
        public ReactiveProperty<int> Food { get; set; }
        public ReactiveProperty<int> Coins { get; set; }

        public PlayerComponent()
        {
            Food = new IntReactiveProperty();
            Coins = new IntReactiveProperty();
        }

        public void Dispose()
        {
            Food.Dispose();
            Coins.Dispose();
        }
    }
}