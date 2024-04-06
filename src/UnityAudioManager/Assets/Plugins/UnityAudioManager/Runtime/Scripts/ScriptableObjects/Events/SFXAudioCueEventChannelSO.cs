using Cysharp.Threading.Tasks;
using long18.ExtensionsCore.Events.ScriptableObjects;
using UnityEngine;

namespace long18.AudioManager.ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "SFXAudioCueEvent",menuName = "long18/Audio Manager/Events/SFX Audio Cue Event Channel")]
    public class SFXAudioCueEventChannelSO : GenericReturnEventChannelSO<SFXAudioCueSO, UniTask<AudioEmitter>>
    {}
}