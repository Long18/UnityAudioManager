using Long18.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Long18.System.Audio.Settings
{
    public class AudioSettingSO : ScriptableObject
    {
        [SerializeField, HideInInspector] private float _volume = 1f;

        public event UnityAction<float> OnVolumeChanged;

        public float Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                OnVolumeChanged.SafeInvoke(_volume);
            }
        }
    }
}