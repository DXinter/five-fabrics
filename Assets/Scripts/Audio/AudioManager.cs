using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private AudioSource musicSource;
        private AudioSource sfxSource;

        private const string MusicVolumeKey = "MusicVolume";
        private const string SfxVolumeKey = "SfxVolume";

        private void Awake()
        {
            var musicObj = new GameObject("MusicSource");
            if (musicSource == null)
            {
                musicObj.transform.SetParent(transform);
                musicSource = musicObj.AddComponent<AudioSource>();
            }

            var sfxObj = new GameObject("SFXSource");
            if (sfxSource == null)
            {
                sfxObj.transform.SetParent(transform);
                sfxSource = sfxObj.AddComponent<AudioSource>();
            }

            LoadVolumeSettings();
        }

        private void OnEnable()
        {
            PlayMusic("busy-bees");
        }

        public void SetMusicVolume(float volume)
        {
            musicSource.volume = volume;
            PlayerPrefs.SetFloat(MusicVolumeKey, volume);
            PlayerPrefs.Save();
        }

        public void SetSfxVolume(float volume)
        {
            sfxSource.volume = volume;
            PlayerPrefs.SetFloat(SfxVolumeKey, volume);
            PlayerPrefs.Save();
        }

        public void PlaySFX(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }

        public void PlayMusic(string musicAddress)
        {
            Addressables.LoadAssetAsync<AudioClip>(musicAddress).Completed += OnMusicLoaded;
        }

        public float GetMusicVolume()
        {
            return PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
        }

        public float GetSfxVolume()
        {
            return PlayerPrefs.GetFloat(SfxVolumeKey, 1f);
        }

        private void OnMusicLoaded(AsyncOperationHandle<AudioClip> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                musicSource.clip = handle.Result;
                musicSource.loop = true;
                musicSource.Play();
            }
            else
            {
                Debug.LogError("Ошибка загрузки музыки из Addressables: " + handle.Status);
            }
        }

        private void LoadVolumeSettings()
        {
            musicSource.volume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
            sfxSource.volume = PlayerPrefs.GetFloat(SfxVolumeKey, 1f);
        }
    }
}