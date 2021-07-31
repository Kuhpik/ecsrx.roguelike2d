using Game.Handlers.Collision;
using Game.Services;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Assets.Installers
{
    public class EntityCollisionInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EntityCollisionListener>().AsSingle().NonLazy();
            CreateHandlers();
        }

        void CreateHandlers()
        {
            var type = typeof(CollisionHander);
            var collection = type.Assembly.GetTypes().Where(x => x.IsSubclassOf(type));

            foreach (var item in collection)
            {
                Container.Bind(item).AsSingle().NonLazy();
            }
        }
    }
}