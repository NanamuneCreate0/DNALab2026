using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(WorldObject), true)]
public class WorldObjectEditor : Editor//WorldObjectÇÃCreatureCellÇæÇØÇ™ëŒè€
{
    private bool showCells = true;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // cellsà»äOÇï`âÊ
        SerializedProperty prop = serializedObject.GetIterator();
        bool enterChildren = true;

        while (prop.NextVisible(enterChildren))
        {
            enterChildren = false;

            if (prop.name == "cells")
                continue;

            EditorGUILayout.PropertyField(prop, true);
        }

        serializedObject.ApplyModifiedProperties();

        DrawCells();
    }

    void DrawCells()
    {
        var obj = target as WorldObject;

        var field = obj.GetType().GetField(
            "cells",
            BindingFlags.NonPublic | BindingFlags.Instance);

        if (field == null) return;

        var list = field.GetValue(obj) as IList<CreatureCell>;
        if (list == null) return;

        EditorGUILayout.Space();

        // ê‹ÇËÇΩÇΩÇ›
        showCells = EditorGUILayout.Foldout(
            showCells,
            $"Cell Debugs ({list.Count})",
            true);

        if (!showCells)
            return;

        foreach (var cell in list)
        {
            if (cell == null) continue;

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.LabelField(cell.GetType().Name);

            DrawSOFields(cell);

            EditorGUILayout.EndVertical();
        }
    }

    void DrawSOFields(ScriptableObject so)
    {
        SerializedObject soObj = new SerializedObject(so);
        SerializedProperty prop = soObj.GetIterator();

        bool enterChildren = true;

        while (prop.NextVisible(enterChildren))
        {
            enterChildren = false;

            if (prop.name == "m_Script")
                continue;

            EditorGUILayout.PropertyField(prop, true);
        }

        soObj.ApplyModifiedProperties();
    }
}