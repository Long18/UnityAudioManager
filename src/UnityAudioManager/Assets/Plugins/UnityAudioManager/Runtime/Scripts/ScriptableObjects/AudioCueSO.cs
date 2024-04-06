
using Cysharp.Threading.Tasks;
using long18.ExtensionsCore.AssetReferences;
using UnityEngine;

namespace long18.AudioManager.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AudioCue", menuName = "long18/Audio Manager/Audio Cue")]
    public class AudioCueSO : ScriptableObject
    {
        [SerializeField] private AudioAssetReference[] _audioClips = default;
        public AudioAssetReference[] AudioClips => _audioClips;

        [Tooltip("Leave if null to use default audio config")]
        [field: SerializeField] public AudioConfigSO AudioConfigSO { get; private set; }
    }
}