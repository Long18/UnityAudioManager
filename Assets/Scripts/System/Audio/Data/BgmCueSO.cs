using UnityEngine;

namespace Long18.System.Audio.Data
{
    [CreateAssetMenu(menuName = "Audio/Bgm Cue", fileName = "BgmCueSO")]
    public class BgmCueSO : AudioCueSO
    {
        public BgmCueSO() => IsLooping = true;
    }
}