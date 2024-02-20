using Long18.AudioSystem.Data;
using Long18.AudioSystem.Emitters;
using Long18.AudioSystem.Helper;
using Long18.AudioSystem.Settings;
using UnityEngine;

namespace Long18.AudioSystem
{
    [RequireComponent((typeof(AudioEmitterPool)))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioCueEventChannelSO _bgmEventChannel;
        [SerializeField] private AudioCueEventChannelSO _sfxEventChannel;

        [Header("Settings")] [SerializeField] private AudioSettingSO _audioSetting;

        private AudioEmitterPool _pool;
        private AudioEmitter _audioEmitter;
        private AudioCueSO _currentBgmCue;
        private AudioCueSO _currentSfxCue;

        private void Awake()
        {
            _pool ??= GetComponent<AudioEmitterPool>();
            _pool.Create();
        }

        private void OnEnable()
        {
            _bgmEventChannel.OnRequested += OnPlayBGM;
            _sfxEventChannel.OnRequested += OnPlaySFX;


            _audioSetting.OnVolumeChanged += OnChangeVolume;
        }

        private void OnDisable()
        {
            _bgmEventChannel.OnRequested -= OnPlayBGM;
            _sfxEventChannel.OnRequested -= OnPlaySFX;

            _audioSetting.OnVolumeChanged -= OnChangeVolume;
        }

        public void OnPlayBGM(AudioCueSO audioToPlay, bool requestPlay) => PlayMusic(audioToPlay, requestPlay);
        public void OnPlaySFX(AudioCueSO audioToPlay, bool requestPlay) => PlaySFX(audioToPlay, requestPlay);

        private void PlaySFX(AudioCueSO audioToPlay, bool requestPlay)
        {
            if (requestPlay)
            {
                HandleSfxToPlay(audioToPlay);
                return;
            }

            HandleSfxToStop();
        }

        private void HandleSfxToPlay(AudioCueSO audioToPlay)
        {
            AudioHelper.TryToLoadData(audioToPlay, OnAudioClipLoaded);
            return;

            void OnAudioClipLoaded(AudioClip currentClip)
            {
                var audioEmitter = _pool.Request();
                if (!audioEmitter)
                {
                    Debug.Log($"[AudioManager::HandleSfxToPlay] Cannot play " +
                              $"{audioToPlay}, no sound emitters available.");
                    return;
                }

                audioEmitter.PlayAudioClip(currentClip, _audioSetting, audioToPlay.IsLooping);
                if (!audioToPlay.IsLooping) audioEmitter.OnFinishedPlaying += AudioFinishedPlaying;

                _currentSfxCue = audioToPlay;
            }
        }

        /// <summary>
        /// All SFX are one shot, so we can release the emitter back to the pool
        /// Let the pool destroy it if it's not needed anymore
        /// </summary>
        private void HandleSfxToStop()
        {
        }

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

            AudioHelper.TryToLoadData(audioToPlay, OnAudioClipLoaded);
            return;

            void OnAudioClipLoaded(AudioClip currentClip)
            {
                if (IsAudioPlaying())
                {
                    AudioClip musicToPlay = currentClip;
                    if (_audioEmitter.GetClip() == musicToPlay) return;
                    startTime = _audioEmitter.FadeMusicOut();
                }

                _audioEmitter = _pool.Request();
                _audioEmitter.FadeMusicIn(currentClip, _audioSetting);

                _currentBgmCue = audioToPlay;
            }
        }

        private void OnChangeVolume(float value)
        {
            if (!IsAudioPlaying()) return;

            _audioEmitter.SetVolume(value);
        }

        private void HandleMusicToStop()
        {
            if (!IsAudioPlaying()) return;

            _audioEmitter.Stop();
        }

        private void AudioFinishedPlaying(AudioEmitterValue audioEmitterValue)
        {
            StopAndCleanEmitter(audioEmitterValue);
        }

        private void StopAndCleanEmitter(AudioEmitterValue audioEmitterValue)
        {
            audioEmitterValue.UnregisterEvent(AudioFinishedPlaying);
            audioEmitterValue.Stop();
            audioEmitterValue.ReleaseToPool();

            if (!_currentSfxCue) _currentSfxCue.GetPlayableAsset().ReleaseAsset();
        }

        private bool IsAudioPlaying() => _audioEmitter != null && _audioEmitter.IsPlaying();
    }
}