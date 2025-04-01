using Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu
{
    public class SettingsMenu : BaseMenu
    {
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        private const string MusicVolumeKey = "MusicVolume";
        private const string SfxVolumeKey = "SfxVolume";
        private AudioManager _audioManager;

        [Inject]
        public void Construct(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        private void Start()
        {
            musicSlider.value = _audioManager.GetMusicVolume();
            sfxSlider.value = _audioManager.GetSfxVolume();
        }

        private void OnEnable()
        {
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        }

        private void OnDisable()
        {
            musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
            sfxSlider.onValueChanged.RemoveListener(SetSfxVolume);
        }

        private void SetMusicVolume(float volume)
        {
            _audioManager.SetMusicVolume(volume);
        }

        private void SetSfxVolume(float volume)
        {
            _audioManager.SetSfxVolume(volume);
        }
    }
}