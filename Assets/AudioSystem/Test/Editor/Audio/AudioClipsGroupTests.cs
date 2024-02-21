using Long18.AudioSystem.Data;
using Long18.AudioSystem.Data.Utils;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Long18.AudioSystem.Test.Editor.Audio
{
    public class AudioClipsGroupTests
    {
        private const int NUMBER_OF_TRY = 10;
        private const int FEW_OF_CLIPS = 2;
        private const int MANY_OF_CLIPS = 5;
        private const int NUMBER_OF_PASS = 2;

        private AssetReferenceT<AudioClip>[] _clips;
        private AudioClipsGroup _group;

        [TearDown]
        public void TearDown()
        {
            foreach (var clip in _clips)
            {
                if (clip == null || !clip.RuntimeKeyIsValid()) continue;
                clip.ReleaseAsset();
            }
        }

        [TestCase(ESequenceMode.Sequential)]
        [TestCase(ESequenceMode.Repeat)]
        [TestCase(ESequenceMode.Random)]
        public void AudioClipsGroup_GetNextClip_ExpectNotIndexOutOfRange(ESequenceMode mode)
        {
            _clips = CreateEmptyAudioClips(FEW_OF_CLIPS);
            _group = new(_clips, mode);
            ChangeClipsContinuously();
        }

        [Test]
        public void AudioClipsGroup_GetPreviousClip_ExpectNotIndexOutOfRange()
        {
            _clips = CreateEmptyAudioClips(FEW_OF_CLIPS);
            _group = new(_clips, ESequenceMode.Sequential);

            for (int i = 0; i < NUMBER_OF_PASS; i++) ChangeClipsBackward();
        }

        [Test]
        public void AudioClipsGroup_GetNextClipSequentially_ExpectCorrectSequence()
        {
            _clips = CreateEmptyAudioClips(MANY_OF_CLIPS);
            _group = new(_clips, ESequenceMode.Sequential);

            for (int i = 0; i < NUMBER_OF_PASS; i++) TravelOnePassThroughClips();
        }

        private void ChangeClipsContinuously()
        {
            for (int i = 0; i < NUMBER_OF_TRY; i++) _group.SwitchToNextClip();
        }

        private void ChangeClipsBackward()
        {
            for (int i = _clips.Length - 1; i >= 0; i--)
            {
                var expectedClip = _clips[i];
                _group.SwitchToPreviousClip();
                Assert.AreEqual(expectedClip, _group.CurrentClip);
            }
        }

        private void TravelOnePassThroughClips()
        {
            foreach (var clip in _clips)
            {
                Assert.That(_group.CurrentClip == clip);
                _group.SwitchToNextClip();
            }
        }

        private AssetReferenceT<AudioClip>[] CreateEmptyAudioClips(int quantity)
        {
            var newClips = new AssetReferenceT<AudioClip>[quantity];
            for (int i = 0; i < quantity; i++)
            {
                newClips[i] = new AssetReferenceT<AudioClip>("AudioClip" + i);
            }

            return newClips;
        }
    }
}