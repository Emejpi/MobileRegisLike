using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Card))]
public class CardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Card myScript = (Card)target;
        if (GUILayout.Button("Flip"))
        {
            myScript.Flip();
        }
        if (GUILayout.Button("Remove"))
        {
            myScript.Flip(new Vector3(10,5,0));
        }
    }
}
