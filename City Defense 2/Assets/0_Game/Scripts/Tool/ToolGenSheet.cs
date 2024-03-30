//#if UNITY_EDITOR
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(ToolReadSheet))]
//[CanEditMultipleObjects]
//public class ToolGenSheet : EditorWindow
//{
//    SerializedProperty m_IntProp;

//    private ToolReadSheet toolReadSheet;

//    [MenuItem("Tools/Gen Sheet")]
//    public static void ShowWindow()
//    {
//        EditorWindow.GetWindow(typeof(ToolGenSheet));

//        m_IntProp = GameObject.FindProperty("m_MyInt");
//    }

//    private void OnGUI()
//    {
//        source = EditorGUILayout.PropertyField(m_IntProp, new GUIContent("ToolReadSheet"));

//        serializedObject.ApplyModifiedProperties();
//    }
//}

//#endif