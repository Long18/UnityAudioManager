using UnityEngine;

namespace Long18.System.Audio.Emitters
{
    [RequireComponent((typeof(AudioSource)))]
    public class AudioEmitter : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public void Resume() => _audioSource.Play();
        public void Pause() => _audioSource.Pause();
        public void Stop() => _audioSource.Stop();
        public AudioClip GetClip() => _audioSource.clip;
        public bool IsPlaying() => _audioSource.isPlaying;
    }
}