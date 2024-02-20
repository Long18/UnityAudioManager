using System;
using UnityEngine.Events;

namespace Long18.AudioSystem.Emitters
{
    [Serializable]
    public struct AudioEmitterValue
    {
        private AudioEmitter _audioEmitter;
        public AudioEmitterValue(AudioEmitter audioEmitter) => _audioEmitter = audioEmitter;

        public void UnregisterEvent(UnityAction<AudioEmitterValue> audioFinishedPlaying)
        {
            if (_audioEmitter) _audioEmitter.OnFinishedPlaying -= audioFinishedPlaying;
        }

        public void StopAudio()
        {
            if (_audioEmitter) _audioEmitter.Stop();
        }

        public void ReleaseToPool()
        {
            if (_audioEmitter) _audioEmitter.ReleasePool();
        }

        public void Stop()
        {
            if (_audioEmitter) _audioEmitter.Stop();
        }
    }
}