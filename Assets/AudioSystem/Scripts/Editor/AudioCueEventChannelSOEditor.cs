using System.Collections;
using Long18.AudioSystem.Data;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Long18Editor.AudioSystem
{
    [CustomEditor(typeof(AudioCueEventChannelSO))]
    public class AudioCueEventChannelSOEditor : Editor
    {
        private const int FRAMES_TO_WAIT = 360;

        [SerializeField] private VisualTreeAsset _visualTreeAsset;

        private AudioCueEventChannelSO Target => target as AudioCueEventChannelSO;

        private VisualElement _bgmElement;
        private VisualElement _sfxElement;
        private ObjectField _bgmData;
        private ObjectField _sfxData;
        private IntegerField _sfxRandomCount;
        private Toggle _isPlayRequest;
        private Toggle _isSfx;
        private Toggle _isAuto;
        private Button _playButton;

        private bool _isSfxRequest;

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            _visualTreeAsset.CloneTree(root);

            _bgmElement = root.Q<VisualElement>("bgm-element");
            _sfxElement = root.Q<VisualElement>("sfx-element");

            _bgmData = root.Q<ObjectField>("bgm-data");
            _sfxData = root.Q<ObjectField>("sfx-data");

            _sfxRandomCount = root.Q<IntegerField>("sfx-random-value");

            _isPlayRequest = root.Q<Toggle>("audio-request");
            _isSfx = root.Q<Toggle>("is-sfx");
            _isAuto = root.Q<Toggle>("is-random");

            _playButton = root.Q<Button>("play-audio-button");

            _isSfx.RegisterValueChangedCallback(ValidAudioDataInterfaceCallback);
            _bgmData.RegisterValueChangedCallback(ValidAudioButtonInterfaceCallback);
            _sfxData.RegisterValueChangedCallback(ValidAudioButtonInterfaceCallback);

            _playButton.clicked += PlayAudio;
            _playButton.SetEnabled(_bgmData.value != null || _sfxData.value != null);

            return root;
        }

        private void ValidAudioButtonInterfaceCallback(ChangeEvent<Object> evt)
        {
            bool isValid = evt.newValue != null || _sfxData.value != null;
            _playButton.SetEnabled(isValid);
        }

        private void ValidAudioDataInterfaceCallback(ChangeEvent<bool> evt)
        {
            _isSfxRequest = evt.newValue;
            _bgmElement.style.display = _isSfxRequest ? DisplayStyle.None : DisplayStyle.Flex;
            _sfxElement.style.display = _isSfxRequest ? DisplayStyle.Flex : DisplayStyle.None;
        }

        private void PlayAudio()
        {
            AudioCueSO value = _bgmData.value as AudioCueSO;
            bool request = _isPlayRequest.value;

            if (_isSfxRequest) value = _sfxData.value as AudioCueSO;
            if (!_isAuto.value)
            {
                Target.PlayAudio(value, request);
                return;
            }

            for (int i = 0; i < _sfxRandomCount.value; i++)
            {
                int framesToWait = FRAMES_TO_WAIT;
                while (framesToWait >= 0)
                {
                    framesToWait--;
                    EditorApplication.QueuePlayerLoopUpdate();
                }

                Target.PlayAudio(value, request);
            }
        }
    }
}