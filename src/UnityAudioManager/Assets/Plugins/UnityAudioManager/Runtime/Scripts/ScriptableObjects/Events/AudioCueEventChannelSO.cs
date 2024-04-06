using Cysharp.Threading.Tasks;
using long18.ExtensionsCore.Events.ScriptableObjects;
using UnityEngine;

namespace long18.AudioManager.ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "AudioCueEvent", menuName = "long18/Audio Manager/Events/Audio Cue Event Channel")]
    public class AudioCueEventChannelSO : GenericReturnEventChannelSO<AudioCueSO, UniTask<AudioEmitter>>
    {}
}