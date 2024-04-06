using long18.AudioManager.ScriptableObjects;
using long18.AudioManager.ScriptableObjects.Events;
using UnityEngine;

namespace long18.AudioManager.AudioChannels
{
    public abstract class BaseAudioChannel : IAudioChannel
    {
        protected AudioManager _audioManager;

        public virtual void Enable(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public virtual void Disable() { }

        protected AudioEmitter GetAudioEmitter()
        {
            var pool = _audioManager.AudioEmitterPool;
            var audioEmitter = pool.GetItem();
            audioEmitter.Pool = pool;
            return audioEmitter;
        }
    }
}