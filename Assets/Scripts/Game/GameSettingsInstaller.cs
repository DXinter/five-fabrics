using Settings;
using UnityEngine;
using Zenject;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public GameInstaller.Settings gameSettings;
        public ItemsSettings itemsSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(gameSettings).IfNotBound();
            Container.BindInstance(itemsSettings).IfNotBound();
        }
    }
}