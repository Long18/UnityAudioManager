using Long18.System.Audio.Data;
using Long18.System.Audio.Emitters;
using Long18.System.Audio.Helper;
using UnityEngine;

namespace Long18.System.Audio
{
    public class AudioManager : MonoBehaviour

    {
        [SerializeField] private AudioCueEventChannelSO _musicEventChannel;
        [SerializeField] private AudioEmitterPool _pool;

        private AudioEmitter _musicEmitter;
        private AudioCueSO _currentBgmCue;

        private void Awake() => _pool.Create();

        private void OnEnable()
        {
            _musicEventChannel.OnRequested += OnPlayMusic;
        }

        private void OnDisable()
        {
            _musicEventChannel.OnRequested -= OnPlayMusic;
        }

        public void OnPlayMusic(AudioCueSO audioToPlay, bool requestPlay) => PlayMusic(audioToPlay, requestPlay);

        private void PlayMusic(AudioCueSO audioToPlay, bool requestPlay)
        {
            if (requestPlay)
            {
                HandleMusicToPlay(audioToPlay);
                return;
            }

            HandleMusicToStop();
        }

        private void HandleMusicToPlay(AudioCueSO audioToPlay)
        {
            float startTime = 0f;

            if (_currentBgmCue != null)
            {
                _currentBgmCue.GetPlayableAsset().ReleaseAsset();
            }

            void OnAudioClipLoaded(AudioClip currentClip)
            {
                if (IsAudioPlaying())
                {
                    AudioClip musicToPlay = currentClip;
                    if (_musicEmitter.GetClip() == musicToPlay) return;
                    startTime = _musicEmitter.FadeMusicOut();
                }

                if (!_musicEmitter) _musicEmitter = _pool.Request();
                _musicEmitter.FadeMusicIn(currentClip, startTime);

                _currentBgmCue = audioToPlay;

                Debug.Log($"[AudioManager::HandleMusicToPlay] Playing background music: {audioToPlay.name}");
            }

            AudioHelper.TryToLoadData(audioToPlay, OnAudioClipLoaded);
        }


        private void HandleMusicToStop()
        {
            if (!IsAudioPlaying()) return;

            _musicEmitter.Stop();
            Debug.Log($"[AudioManager] Stopped playing background music");
        }


        private bool IsAudioPlaying() => _musicEmitter != null && _musicEmitter.IsPlaying();
    }
}