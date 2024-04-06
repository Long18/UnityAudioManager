using long18.AudioManager.ScriptableObjects;
using UnityEngine;

namespace long18.AudioManager.AudioChannels
{
    public interface IAudioChannel
    {
        void Enable(AudioManager audioManager);
        void Disable();
    }
}