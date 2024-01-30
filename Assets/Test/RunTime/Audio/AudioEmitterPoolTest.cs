using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Long18.System.Audio;
using Long18.System.Audio.Data;
using Long18.System.Audio.Emitters;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Long18.Test.RunTime.Audio
{
    [TestFixture]
    [Category("Integration")]
    public class AudioEmitterPoolTest
    {
        private const string AUDIO_TEST_SCENE_PATH = "Assets/Scenes/WIP/AudioTestScene.unity";
        private const string BGM_EVENT_CHANNEL_PATH = "Assets/Events/Audio/PlayBGMChannel.asset";

        private const float TIME_TO_WAIT = 5.01f;

        private PlayBgmOnSceneStart _objectTriggerBgm;
        private List<BgmCueSO> _bgmCue;
        private AudioCueEventChannelSO _bgmEvent;

        [UnitySetUp]
        public IEnumerator OneTimeSetup()
        {
            var audioBgmCueGUIDs = AssetDatabase.FindAssets($"t:{typeof(BgmCueSO)}");

            _bgmCue = new();

            for (var index = 0; index < 2; index++)
            {
                var guid = audioBgmCueGUIDs[index];
                var audio = AssetDatabase.LoadAssetAtPath<BgmCueSO>(AssetDatabase.GUIDToAssetPath(guid));
                _bgmCue.Add(audio);
            }

            _bgmEvent = AssetDatabase.LoadAssetAtPath<AudioCueEventChannelSO>(BGM_EVENT_CHANNEL_PATH);

            Assert.NotNull(_bgmEvent, $"Event bgm not null");

            yield return EditorSceneManager.LoadSceneInPlayMode(AUDIO_TEST_SCENE_PATH,
                new LoadSceneParameters(LoadSceneMode.Single));

            AudioManager audioManager = Object.FindObjectOfType<AudioManager>();

            Assert.NotNull(audioManager, $"Object {audioManager.name} is not null");

            _objectTriggerBgm = Object.FindObjectOfType<PlayBgmOnSceneStart>();
            _objectTriggerBgm.gameObject.SetActive(false);
        }

        [UnityTest]
        public IEnumerator PlayBGM_ShouldPlayAudio_ReturnFalseIfFirstIsDeactivated()
        {
            _objectTriggerBgm.gameObject.SetActive(true);

            _bgmEvent.PlayAudio(_bgmCue[0]);

            yield return new WaitForSeconds(TIME_TO_WAIT);

            _bgmEvent.PlayAudio(_bgmCue[1]);

            yield return new WaitForSeconds(TIME_TO_WAIT);

            AudioEmitter[] emitters = Object.FindObjectsOfType<AudioEmitter>();

            bool isAnyDeactivated = emitters.Any(emitter => !emitter.gameObject.activeSelf);

            Assert.IsFalse(isAnyDeactivated, "At least one AudioEmitter should be deactivated");
        }

    }
}