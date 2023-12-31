using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(UIComponentData))]
public class InspectorEditor : Editor
{
    private ReorderableList reorderableList;

    private void OnEnable()
    {
        reorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("m_objects"), true, true, true, true);

        reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            // 绘制默认属性字段
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width - 30, EditorGUIUtility.singleLineHeight), element, GUIContent.none);

            // 添加按钮
            if (GUI.Button(new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight), "Button"))
            {
                // 处理按钮点击事件，可以在这里执行你的操作
                Debug.Log("Button clicked for element " + index);
            }
        };

        reorderableList.elementHeightCallback = (int index) =>
        {
            var element = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
            return EditorGUI.GetPropertyHeight(element);
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        reorderableList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }
}