using Long18.System.Audio.Data;
using Long18.System.Audio.Emitters;
using Long18.System.Audio.Helper;
using UnityEngine;

namespace Long18.System.Audio
{
    public class AudioManager : MonoBehaviour

    {
        [SerializeField] private AudioCueEventChannelSO _musicEventChannel;

        private AudioEmitter _playingMusicAudioEmitter;
        private AudioCueSO _currentBgmCue;

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
            if (_currentBgmCue != null)
            {
                _currentBgmCue.GetPlayableAsset().ReleaseAsset();
            }

            AudioHelper.TryToLoadData(audioToPlay, currentClip =>
            {
                if (IsAudioPlaying())
                {
                    AudioClip musicToPlay = currentClip;
                    if (_playingMusicAudioEmitter.GetClip() == musicToPlay) return;
                }

                if (_playingMusicAudioEmitter == null)
                {
                    // TODO: Get new audio
                }

                _currentBgmCue = audioToPlay;

                Debug.Log($"[AudioManager::HandleMusicToPlay] Playing background music: {audioToPlay.name}");
            });
        }


        private void HandleMusicToStop()
        {
            if (!IsAudioPlaying()) return;

            _playingMusicAudioEmitter.Stop();
            Debug.Log($"[AudioManager] Stopped playing background music");
        }


        private bool IsAudioPlaying() => _playingMusicAudioEmitter != null && _playingMusicAudioEmitter.IsPlaying();
    }
}