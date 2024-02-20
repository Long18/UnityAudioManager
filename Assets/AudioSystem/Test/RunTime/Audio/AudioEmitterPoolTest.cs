using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Long18.AudioSystem.Data;
using Long18.AudioSystem.Emitters;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Long18.AudioSystem.Test.RunTime.Audio
{
    [TestFixture]
    [Category("AudioIntegration")]
    public class AudioEmitterPoolTest
    {
        private const string AUDIO_TEST_SCENE_PATH = "Assets/Scenes/WIP/AudioTestScene.unity";
        private const string BGM_EVENT_CHANNEL_PATH = "Assets/Events/Audio/PlayBGMChannel.asset";
        private const string SFX_EVENT_CHANNEL_PATH = "Assets/Events/Audio/PlaySFXChannel.asset";
        private const int NUMBER_OF_BGM_ASSET = 2;
        private const int NUMBER_OF_SFX_ASSET = 3;
        private const float TIME_TO_WAIT = 5.01f;

        private List<BgmCueSO> _bgmCue;
        private List<SfxCueSO> _sfxCue;
        private AudioCueEventChannelSO _bgmEvent;
        private AudioCueEventChannelSO _sfxEvent;
        private PlayBgmOnSceneStart[] _objectTriggerBgm;
        private PlaySfx[] _objectTriggerSfx;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            LoadBgmAndSfxCues();

            LoadEventChannels();

            yield return LoadSceneAndInitializeObjects();
        }

        private void LoadBgmAndSfxCues()
        {
            _bgmCue = LoadAssets<BgmCueSO>(NUMBER_OF_BGM_ASSET);
            _sfxCue = LoadAssets<SfxCueSO>(NUMBER_OF_SFX_ASSET);
        }

        private void LoadEventChannels()
        {
            _bgmEvent = AssetDatabase.LoadAssetAtPath<AudioCueEventChannelSO>(BGM_EVENT_CHANNEL_PATH);
            _sfxEvent = AssetDatabase.LoadAssetAtPath<AudioCueEventChannelSO>(SFX_EVENT_CHANNEL_PATH);

            Assert.NotNull(_bgmEvent, $"BGM event channel not found");
            Assert.NotNull(_sfxEvent, $"SFX event channel not found");
        }

        private IEnumerator LoadSceneAndInitializeObjects()
        {
            yield return EditorSceneManager.LoadSceneInPlayMode(AUDIO_TEST_SCENE_PATH,
                new LoadSceneParameters(LoadSceneMode.Single));

            AudioManager audioManager = Object.FindObjectOfType<AudioManager>();

            Assert.NotNull(audioManager, $"AudioManager object not found");

            _objectTriggerBgm = Resources.FindObjectsOfTypeAll<PlayBgmOnSceneStart>();
            _objectTriggerSfx = Resources.FindObjectsOfTypeAll<PlaySfx>();

            _objectTriggerBgm[0].gameObject.SetActive(false);
            _objectTriggerSfx[0].gameObject.SetActive(false);
        }

        private List<TCue> LoadAssets<TCue>(int numberOfAssets) where TCue : ScriptableObject
        {
            string[] audioCueGUIDs = AssetDatabase.FindAssets($"t:{typeof(TCue)}");
            List<TCue> loadedAssets = new List<TCue>();

            for (int index = 0; index < numberOfAssets; index++)
            {
                string guid = audioCueGUIDs[index];
                TCue audio = AssetDatabase.LoadAssetAtPath<TCue>(AssetDatabase.GUIDToAssetPath(guid));
                loadedAssets.Add(audio);
            }

            return loadedAssets;
        }

        [UnityTest]
        public IEnumerator PlayBGM_ShouldPlayAudio_ShouldReturnTrueIfAnyEmitterDeactivated()
        {
            _objectTriggerBgm[0].gameObject.SetActive(true);

            for (int i = 0; i < NUMBER_OF_BGM_ASSET; i++)
            {
                _bgmEvent.PlayAudio(_bgmCue[i]);
                yield return new WaitForSeconds(TIME_TO_WAIT);
            }

            AudioEmitter[] emitters = Object.FindObjectsOfType<AudioEmitter>();

            bool isAnyDeactivated = emitters.Any(emitter => emitter.gameObject.activeSelf);

            Assert.IsTrue(isAnyDeactivated, "At least one AudioEmitter should be deactivated");
        }

        [TestCase(10, ExpectedResult = null)]
        [TestCase(7, ExpectedResult = null)]
        [TestCase(3, ExpectedResult = null)]
        [TestCase(5, ExpectedResult = null)]
        [TestCase(9, ExpectedResult = null)]
        public IEnumerator PlaySFX_ShouldPlayAudio_ShouldReturnTrueIfAllEmittersDeactivated_WithNumberOfTime(int value)
        {
            _objectTriggerSfx[0].gameObject.SetActive(true);

            for (int i = 0; i < value; i++)
            {
                var randomOfNumber = Random.Range(0, NUMBER_OF_SFX_ASSET - 1);
                yield return new WaitForSeconds(1);
                _sfxEvent.PlayAudio(_sfxCue[randomOfNumber]);
            }

            AudioEmitter[] emitters = Object.FindObjectsOfType<AudioEmitter>();

            bool isAllEmittersDeactivated = emitters.All(emitter => emitter.gameObject.activeSelf);

            Assert.IsTrue(isAllEmittersDeactivated, "All emitters should be deactivated");
        }
    }
}