using System;
using System.Collections.Generic;
using Items;
using Zenject;

namespace Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallManagers();
            InstallServices();
            InstallFactories();
            InstallPools();
        }
        
        private void InstallManagers()
        {
            var managers = new List<Type>
            {
            };

            foreach (var manager in managers)
            {
                Container.BindInterfacesAndSelfTo(manager)
                    .FromNewComponentOnNewGameObject().UnderTransformGroup("Managers").AsSingle().NonLazy();
            }
        }

        private void InstallServices()
        {
            var services = new List<Type>
            {
                typeof(ItemRegistry),
            };

            foreach (var service in services)
            {
                Container.BindInterfacesAndSelfTo(service).AsSingle().NonLazy();
            }
        }


        private void InstallFactories()
        {
        }

        private void InstallPools()
        {
        }

        [Serializable]
        public class Settings
        {
        }
    }
}