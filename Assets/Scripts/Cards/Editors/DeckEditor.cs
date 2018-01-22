using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Deck))]
public class DeckEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Deck myScript = (Deck)target;
        if (GUILayout.Button("Flip"))
        {
            myScript.FlipTop();
        }
        if (GUILayout.Button("Remove"))
        {
            myScript.RemoveTop();
            myScript.FlipTop();
        }
    }
}
