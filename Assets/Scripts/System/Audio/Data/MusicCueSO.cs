using UnityEngine;

namespace Long18.System.Audio.Data
{
    [CreateAssetMenu(menuName = "Audio/Music Cue", fileName = "MusicCueSO")]
    public class MusicCueSO : AudioCueSO
    {
        public MusicCueSO() => Looping = true;
    }
}