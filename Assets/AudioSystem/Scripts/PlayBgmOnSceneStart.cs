using Long18.AudioSystem.Data;
using UnityEngine;

namespace Long18.AudioSystem
{
    public class PlayBgmOnSceneStart : MonoBehaviour
    {
        [Header("Raise on")] [SerializeField] private AudioCueEventChannelSO _musicEventChannel;

        [Header("Configs")] public AudioCueSO musicTrack;

        private void Start()
        {
            PlayBackgroundMusic();
        }

        public void PlayBackgroundMusic()
        {
            _musicEventChannel.PlayAudio(musicTrack);
        }

        public void StopBackgroundMusic()
        {
            _musicEventChannel.PlayAudio(musicTrack, false);
        }
    }
}