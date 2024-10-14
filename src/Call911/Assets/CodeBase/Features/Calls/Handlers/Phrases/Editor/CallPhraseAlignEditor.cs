using CodeBase.Features.Calls.Handlers.Phrases.UI;
using UnityEditor;

namespace CodeBase.Features.Calls.Handlers.Phrases.Editor
{
    [CustomEditor(typeof(CallPhraseAlign))]
    public class CallPhraseAlignEditor : UnityEditor.Editor
    {
        private SerializedProperty _isRightMapping;
        private SerializedProperty _mappingSize;
        private SerializedProperty _personName;
        private SerializedProperty _personMessage;

        private void OnEnable()
        {
            _isRightMapping = serializedObject.FindProperty("_isRightMapping");
            _mappingSize = serializedObject.FindProperty("_mappingSize");
            _personName = serializedObject.FindProperty("_personName");
            _personMessage = serializedObject.FindProperty("_personMessage");
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(_isRightMapping);
            EditorGUILayout.PropertyField(_mappingSize);
            EditorGUILayout.PropertyField(_personName);
            EditorGUILayout.PropertyField(_personMessage);
            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck()) 
                AlignPhrase(_isRightMapping.boolValue);
        }

        private void AlignPhrase(bool isRightMapping)
        {
            var align = (CallPhraseAlign) target;
            align.Align(isRightMapping);
        }
    }
}