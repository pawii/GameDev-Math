using System;
using TransformComponents;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(RotationComponent))]
    public class RotationComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty rotationType;

        private SerializedProperty eulerX;
        private SerializedProperty eulerY;
        private SerializedProperty eulerZ;
        
        private SerializedProperty quaternionX;
        private SerializedProperty quaternionY;
        private SerializedProperty quaternionZ;
        private SerializedProperty quaternionW;
        
        private void OnEnable()
        {
            rotationType = serializedObject.FindProperty("rotationType");
            
            eulerX = serializedObject.FindProperty("eulerX");
            eulerY = serializedObject.FindProperty("eulerY");
            eulerZ = serializedObject.FindProperty("eulerZ");
            
            
            quaternionX = serializedObject.FindProperty("quaternionX");
            quaternionY = serializedObject.FindProperty("quaternionY");
            quaternionZ = serializedObject.FindProperty("quaternionZ");
            quaternionW = serializedObject.FindProperty("quaternionW");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.PropertyField(rotationType);
            if (rotationType.enumValueIndex == 0)
            {
                EditorGUILayout.PropertyField(eulerX);
                EditorGUILayout.PropertyField(eulerY);
                EditorGUILayout.PropertyField(eulerZ);
            }
            else if (rotationType.enumValueIndex == 1)
            {
                EditorGUILayout.PropertyField(quaternionX);
                EditorGUILayout.PropertyField(quaternionY);
                EditorGUILayout.PropertyField(quaternionZ);
                
                if (Mathf.Approximately(quaternionX.floatValue, 0f) &&
                    Mathf.Approximately(quaternionY.floatValue, 0f) && 
                    Mathf.Approximately(quaternionZ.floatValue, 0f))
                {
                    EditorGUILayout.LabelField("Direction vector undefined!");
                }
                
                EditorGUILayout.PropertyField(quaternionW);
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
