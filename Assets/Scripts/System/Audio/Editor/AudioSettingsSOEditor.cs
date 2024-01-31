using Long18.System.Audio.Settings;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Long18.Editor.System.Audio.Editor
{
    [CustomEditor(typeof(AudioSettingSO))]
    public class AudioSettingsSOEditor : UnityEditor.Editor
    {
        [SerializeField] private VisualTreeAsset _visualTreeAsset;

        private AudioSettingSO Target => target as AudioSettingSO;
        private Slider volumeSlider;

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            _visualTreeAsset.CloneTree(root);

            volumeSlider = root.Q<Slider>("volume-slider");
            volumeSlider.value = Target.Volume;

            volumeSlider.RegisterValueChangedCallback(ChangeValueEditorCallback);
            Target.OnVolumeChanged += ChangeMasterVolume;

            return root;
        }

        private void ChangeValueEditorCallback(ChangeEvent<float> evt) => Target.Volume = evt.newValue;
        private void ChangeMasterVolume(float value) => volumeSlider.value = value;
        private void OnDisable() => Target.OnVolumeChanged -= ChangeMasterVolume;
    }
}