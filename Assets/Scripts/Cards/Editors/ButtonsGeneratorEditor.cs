using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ButtonsGenerator))]
public class ButtonsGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ButtonsGenerator myScript = (ButtonsGenerator)target;
        if (GUILayout.Button("Generate"))
        {
            //myScript.Generate(new Option(Color.green, "test"));
        }
    }
}
