using UnityEngine;

namespace Long18.AudioSystem.Data
{
    [CreateAssetMenu(menuName = "Audio/Sfx Cue", fileName = "SfxCueSO")]
    public class SfxCueSO : AudioCueSO
    {
        public SfxCueSO() => IsLooping = false;
    }
}