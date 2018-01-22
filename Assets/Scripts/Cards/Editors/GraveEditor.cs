using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Grave))]
public class GraveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Grave myScript = (Grave)target;
        if (GUILayout.Button("Flip"))
        {
            myScript.FlipTop();
        }
        if (GUILayout.Button("Remove"))
        {
            myScript.RemoveTop();
        }
        if (GUILayout.Button("BackToDeck"))
        {
            myScript.FlipAll();
            myScript.RemoveAll();
        }
    }
}
