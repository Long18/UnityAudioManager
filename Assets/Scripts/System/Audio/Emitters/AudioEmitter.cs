using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Long18.System.Audio.Emitters
{
    [RequireComponent((typeof(AudioSource)))]
    public class AudioEmitter : MonoBehaviour
    {
        public event UnityAction<AudioEmitterValue> OnFinishedPlaying;
        private const float DEFAULT_VOLUME = 3f;
        private const float FADE_VOLUME_DURATION = 2f;

        [SerializeField] private AudioSource _audioSource;

        private AudioEmitterValue _emitterValue;

        public void PlayAudioClip(AudioClip clip, bool hasLoop)
        {
            _emitterValue = new AudioEmitterValue(this);

            _audioSource.clip = clip;
            // TODO: Create volume config
            _audioSource.volume = DEFAULT_VOLUME;
            _audioSource.loop = hasLoop;
            _audioSource.time = 0f;
            _audioSource.Play();

            if (hasLoop) return;
            Invoke(nameof(OnFinishedPlay), clip.length);
        }

        public void FadeMusicIn(AudioClip clip, float startTime = 0f)
        {
            PlayAudioClip(clip, true);
            StartCoroutine(FadeIn(0, 0.5f));

            if (startTime <= _audioSource.clip.length)
            {
                _audioSource.time = startTime;
            }

            // TODO: Create volume config
            StartCoroutine(FadeIn(DEFAULT_VOLUME, FADE_VOLUME_DURATION));
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
        }

        public void Resume() => _audioSource.Play();
        public void Pause() => _audioSource.Pause();
        public void Stop() => _audioSource.Stop();
        public AudioClip GetClip() => _audioSource.clip;
        public bool IsPlaying() => _audioSource.isPlaying;
        private void OnFinishedPlay() => OnFinishedPlaying?.Invoke(_emitterValue);
    }
}