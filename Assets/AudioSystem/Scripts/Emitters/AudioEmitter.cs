using System.Collections;
using Long18.AudioSystem.Settings;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace Long18.AudioSystem.Emitters
{
    [RequireComponent((typeof(AudioSource)))]
    public class AudioEmitter : MonoBehaviour
    {
        public event UnityAction<AudioEmitterValue> OnFinishedPlaying;
        private const float FADE_VOLUME_DURATION = 3f;

        [SerializeField] private AudioSource _audioSource;

        private IObjectPool<AudioEmitter> _pool;
        private AudioEmitterValue _emitterValue;

        public void PlayAudioClip(AudioClip clip, AudioSettingSO settings, bool hasLoop)
        {
            _emitterValue = new AudioEmitterValue(this);

            _audioSource.clip = clip;
            _audioSource.volume = settings.Volume;
            _audioSource.loop = hasLoop;
            _audioSource.time = 0f;
            _audioSource.Play();

            if (hasLoop) return;
            Invoke(nameof(OnFinishedPlay), clip.length);
        }

        public void FadeMusicIn(AudioClip clip, AudioSettingSO settings, float startTime = 0f)
        {
            PlayAudioClip(clip, settings, true);
            StartCoroutine(FadeIn(0, 0.5f));

            if (startTime <= _audioSource.clip.length)
            {
                _audioSource.time = startTime;
            }

            StartCoroutine(FadeIn(settings.Volume, FADE_VOLUME_DURATION));
        }

        public float FadeMusicOut()
        {
            StartCoroutine(FadeOut(FADE_VOLUME_DURATION));

            return _audioSource.time;
        }

        private IEnumerator FadeIn(float targetVolume, float duration)
        {
            float currentTime = 0;
            float startVolume = _audioSource.volume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
                yield return null;
            }

            _audioSource.volume = targetVolume;
        }

        private IEnumerator FadeOut(float duration)
        {
            float currentTime = 0;
            float startVolume = _audioSource.volume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(startVolume, 0f, currentTime / duration);
                yield return null;
            }

            _audioSource.volume = 0f;
            OnFinishedPlay();
            ReleasePool();
        }

        public void Resume() => _audioSource.Play();
        public void Pause() => _audioSource.Pause();
        public void Stop() => _audioSource.Stop();
        public void SetVolume(float value) => _audioSource.volume = value;
        public AudioClip GetClip() => _audioSource.clip;
        public float GetVolume() => _audioSource.volume;
        public bool IsPlaying() => _audioSource.isPlaying;
        public void ReleasePool() => _pool?.Release(this);
        public void Init(IObjectPool<AudioEmitter> pool) => _pool = pool;
        private void OnFinishedPlay() => OnFinishedPlaying?.Invoke(_emitterValue);

    }
}