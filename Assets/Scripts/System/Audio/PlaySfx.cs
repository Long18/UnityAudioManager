using System;
using Long18.System.Audio.Data;
using UnityEngine;

namespace Long18.System.Audio
{
    public class PlaySfx : MonoBehaviour
    {
        [Header("Raise on")] [SerializeField] private AudioCueEventChannelSO _sfxEventChannel;

        [Header("Configs")] public SfxCueSO soundEffectTrack;
        [SerializeField] private bool _playOnStart;

        private void Start()
        {
            if (!_playOnStart) return;
            OnPlaySfx();
        }

        public void OnPlaySfx()
        {
            _sfxEventChannel.PlayAudio(soundEffectTrack);
        }

        public void OnStopSfx()
        {
            _sfxEventChannel.PlayAudio(soundEffectTrack, false);
        }
    }
}