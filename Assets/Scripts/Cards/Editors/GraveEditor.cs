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
        if (GUILayout.Button("Generate Sections"))
        {
            myScript.CreateChanceSections(myScript.maxPriory);
        }
        if (GUILayout.Button("Next Priory"))
        {
            myScript.nextPriory = myScript.NextPriory();
        }
        if (GUILayout.Button("BackToDeck"))
        {
            myScript.FlipAll();
            myScript.RemoveAll();
        }
    }
}
