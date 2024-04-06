using System;
using Cysharp.Threading.Tasks;
using long18.AudioManager.Helpers;
using UnityEngine;

namespace long18.AudioManager
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioEmitter : MonoBehaviour
    {
        public event Action AudioStopped;

        [field: SerializeField] 
        public AudioSource AudioSource { get; private set; }

        public AudioEmitterPool Pool { get; set; }

        private float _fadeInTime;
        private float _fadeOutTime;
    
        private void OnValidate()
        {
            if (AudioSource == null)
                AudioSource = GetComponent<AudioSource>();
        }

        public async UniTask Play(AudioClip clip)
        {
            if (clip == null) return;

            AudioSource.clip = clip;
            AudioSource.Play();
            await AudioFadeHelper.FadeIn(AudioSource, new FadeConfig(_fadeInTime));
        }

        public async UniTask Stop()
        {
            await AudioFadeHelper.FadeOut(AudioSource, new FadeConfig(_fadeOutTime));
            AudioStopped?.Invoke();
            Pool.ReleaseItem(this);
        }

        public void SetFadeConfig(float fadeInTime, float fadeOutTime)
        {
            _fadeInTime = fadeInTime;
            _fadeOutTime = fadeOutTime;
        }
    }
}