using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Long18.AudioSystem.Settings;

namespace Long18Editor.AudioSystem
{
    [CustomEditor(typeof(AudioSettingSO))]
    public class AudioSettingsSOEditor : Editor
    {
        [SerializeField] private VisualTreeAsset _visualTreeAsset;

        private AudioSettingSO Target => target as AudioSettingSO;
        private Slider _volumeSlider;

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            _visualTreeAsset.CloneTree(root);

            _volumeSlider = root.Q<Slider>("volume-slider");
            _volumeSlider.value = Target.Volume;

            _volumeSlider.RegisterValueChangedCallback(ChangeValueEditorCallback);
            Target.OnVolumeChanged += ChangeMasterVolume;

            return root;
        }

        private void ChangeValueEditorCallback(ChangeEvent<float> evt) => Target.Volume = evt.newValue;
        private void ChangeMasterVolume(float value) => _volumeSlider.value = value;
        private void OnDisable() => Target.OnVolumeChanged -= ChangeMasterVolume;
    }
}