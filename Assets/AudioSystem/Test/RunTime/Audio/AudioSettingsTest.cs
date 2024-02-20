using System.Collections;
using Long18.AudioSystem.Data;
using Long18.AudioSystem.Emitters;
using Long18.AudioSystem.Settings;
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
    public class AudioSettingsTest
    {
        private const string AUDIO_TEST_SCENE_PATH = "Assets/AudioSystem/Scenes/WIP/AudioTestScene.unity";
        private const string AUDIO_SETTINGS_PATH = "Assets/AudioSystem/Data/Audio/Settings/AudioSettings.asset";
        private const string BGM_EVENT_CHANNEL_PATH = "Assets/AudioSystem/Events/Audio/PlayBGMChannel.asset";

        private const float MAX_VOLUME = 1f;
        private const float MID_VOLUME = 0.5f;
        private const float MIN_VOLUME = 0f;

        private PlayBgmOnSceneStart[] _objectTriggerBgm;

        private AudioCueEventChannelSO _bgmEvent;
        private AudioSettingSO _audioSetting;

        private float _oldVolume;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            LoadEventChannels();
            LoadAudioSettings();

            _oldVolume = _audioSetting.Volume;

            yield return LoadSceneAndInitializeObjects();
        }

        [UnityTearDown]
        public void TearDown() => _audioSetting.Volume = _oldVolume;

        private void LoadEventChannels()
        {
            _bgmEvent = AssetDatabase.LoadAssetAtPath<AudioCueEventChannelSO>(BGM_EVENT_CHANNEL_PATH);
            Assert.NotNull(_bgmEvent, $"BGM event channel not found");
        }

        private void LoadAudioSettings()
        {
            _audioSetting = AssetDatabase.LoadAssetAtPath<AudioSettingSO>(AUDIO_SETTINGS_PATH);
            Assert.NotNull(_audioSetting, $"Audio settings not found");
        }

        private IEnumerator LoadSceneAndInitializeObjects()
        {
            yield return EditorSceneManager.LoadSceneInPlayMode(AUDIO_TEST_SCENE_PATH,
                new LoadSceneParameters(LoadSceneMode.Single));

            AudioManager audioManager = Object.FindObjectOfType<AudioManager>();

            _objectTriggerBgm = Resources.FindObjectsOfTypeAll<PlayBgmOnSceneStart>();
            _objectTriggerBgm[0].gameObject.SetActive(false);

            Assert.NotNull(audioManager, $"AudioManager object not found");
        }

        [TestCase(0.1f, ExpectedResult = null)]
        [TestCase(0.5f, ExpectedResult = null)]
        [TestCase(1f, ExpectedResult = null)]
        public IEnumerator PlayAudio_WithVolumeChange_VolumeOfAudioEmitterShouldEqual(float volume)
        {
            _audioSetting.Volume = volume;

            _objectTriggerBgm[0].gameObject.SetActive(true);

            yield return new WaitForSeconds(5f);
            AudioEmitter emitter = Object.FindObjectOfType<AudioEmitter>();

            float expectedVolume = volume;
            float actualVolume = emitter.GetVolume();

            Assert.AreEqual(expectedVolume,
                actualVolume,
                $"Expected volume: {expectedVolume}, Actual volume: {actualVolume}");
        }

        [UnityTest]
        public IEnumerator ChangeVolume_MultipleTimes_VolumeOfAudioEmitterShouldEqual()
        {
            _audioSetting.Volume = MAX_VOLUME;

            _objectTriggerBgm[0].gameObject.SetActive(true);

            yield return new WaitForSeconds(5f);

            AudioEmitter emitter = Object.FindObjectOfType<AudioEmitter>();

            float expectedVolume = MAX_VOLUME;
            float actualVolume = emitter.GetVolume();

            Assert.AreEqual(expectedVolume,
                actualVolume,
                $"Expected volume: {expectedVolume}, Actual volume: {actualVolume}");

            _audioSetting.Volume = MID_VOLUME;

            expectedVolume = MID_VOLUME;
            actualVolume = emitter.GetVolume();

            Assert.AreEqual(expectedVolume,
                actualVolume,
                $"Expected volume: {expectedVolume}, Actual volume: {actualVolume}");

            _audioSetting.Volume = MIN_VOLUME;

            expectedVolume = MIN_VOLUME;
            actualVolume = emitter.GetVolume();

            Assert.AreEqual(expectedVolume,
                actualVolume,
                $"Expected volume: {expectedVolume}, Actual volume: {actualVolume}");
        }

        [UnityTest]
        public IEnumerator ChangeVolumeAndPause_ThenResume_VolumeOfAudioEmitterShouldEqual()
        {
            _audioSetting.Volume = MAX_VOLUME;

            _objectTriggerBgm[0].gameObject.SetActive(true);

            yield return new WaitForSeconds(5f);

            AudioEmitter emitter = Object.FindObjectOfType<AudioEmitter>();

            float expectedVolume = MAX_VOLUME;
            float actualVolume = emitter.GetVolume();

            Assert.AreEqual(expectedVolume,
                actualVolume,
                $"Expected volume: {expectedVolume}, Actual volume: {actualVolume}");

            _audioSetting.Volume = MID_VOLUME;

            expectedVolume = MID_VOLUME;
            actualVolume = emitter.GetVolume();

            Assert.AreEqual(expectedVolume,
                actualVolume,
                $"Expected volume: {expectedVolume}, Actual volume: {actualVolume}");

            emitter.Pause();

            yield return new WaitForSeconds(2f);

            emitter.Resume();

            expectedVolume = MID_VOLUME;
            actualVolume = emitter.GetVolume();

            Assert.AreEqual(expectedVolume,
                actualVolume,
                $"Expected volume: {expectedVolume}, Actual volume: {actualVolume}");
        }
    }
}