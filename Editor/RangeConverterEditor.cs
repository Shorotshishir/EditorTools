using System;
using UnityEditor;
using UnityEngine;

namespace siratim.Tools
{
  public class RangeConverterEditor : EditorWindow
  {
    private float CurrentValue = 0f;
    private float NewRangeEnd = 1f;
    private float NewRangeStart = 0f;
    private float NewValue = 0f;
    private float OldRangeEnd = 360f;
    private float OldRangeStart = 0f;
    
    [MenuItem("Tools/Converter/Range")]
    public static void ShowWindow()
    {
      GetWindow(typeof(RangeConverterEditor),title:"RangeConverter", utility:false);
    }

    private void OnGUI()
    {
      OldRangeStart = EditorGUILayout.FloatField("Old Range Start", OldRangeStart);
      OldRangeEnd = EditorGUILayout.FloatField("Old Range End", OldRangeEnd);
      NewRangeStart = EditorGUILayout.FloatField("New Range Start", NewRangeStart);
      NewRangeEnd = EditorGUILayout.FloatField("New Range End", NewRangeEnd);
      
      GUILayout.Space(pixels: 15f);
      CurrentValue = EditorGUILayout.FloatField("Current Value", CurrentValue);
      NewValue = EditorGUILayout.FloatField("New Value", NewValue);
    }

    private void OnInspectorUpdate()
    {
      NewValue = RangeConverter.Convert(OldRangeStart,OldRangeEnd, NewRangeStart, NewRangeEnd, CurrentValue);
    }
  }
}