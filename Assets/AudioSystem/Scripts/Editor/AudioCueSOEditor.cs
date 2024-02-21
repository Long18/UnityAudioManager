using Long18.AudioSystem.Data;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Long18Editor.AudioSystem
{
    [CustomEditor(typeof(AudioCueSO), true)]
    public class AudioCueSOEditor : Editor
    {
        [SerializeField] private VisualTreeAsset _visualTreeAsset;

        private AudioCueSO Target => target as AudioCueSO;
        private Button _button;

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            _visualTreeAsset.CloneTree(root);

            _button = root.Q<Button>("play-audio-button");
            _button.SetEnabled(Target.GetClips().Length > 0);
            
            // TODO: Remove after function implementation
            _button.SetEnabled(false);
            
            _button.clicked += PlayAudio;

            return root;
        }

        private void PlayAudio()
        {
            // TODO: Play audio with out event channel
        }
    }
}