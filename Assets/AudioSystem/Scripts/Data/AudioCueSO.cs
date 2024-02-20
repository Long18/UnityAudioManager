using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Long18.AudioSystem.Data
{
    public class AudioCueSO : ScriptableObject
    {
        public bool IsLooping = false;
        [SerializeField] private AudioClipsGroup[] _audioClipGroups = default;

        public AssetReferenceT<AudioClip>[] GetClips()
        {
            int numberOfClips = _audioClipGroups.Length;
            AssetReferenceT<AudioClip>[] clipsResult = new AssetReferenceT<AudioClip>[numberOfClips];

            for (int i = 0; i < numberOfClips; i++)
            {
                clipsResult[i] = _audioClipGroups[i].SwitchToNextClip();
            }

            return clipsResult;
        }

        public AssetReferenceT<AudioClip> GetPlayableAsset()
            => _audioClipGroups[0].SwitchToNextClip();
    }
}