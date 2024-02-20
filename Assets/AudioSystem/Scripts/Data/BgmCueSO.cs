using UnityEngine;

namespace Long18.AudioSystem.Data
{
    [CreateAssetMenu(menuName = "Audio/Bgm Cue", fileName = "BgmCueSO")]
    public class BgmCueSO : AudioCueSO
    {
        public BgmCueSO() => IsLooping = true;
    }
}