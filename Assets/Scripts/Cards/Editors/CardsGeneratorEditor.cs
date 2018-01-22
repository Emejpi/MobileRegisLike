using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CardsGenerator))]
public class CardsGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CardsGenerator myScript = (CardsGenerator)target;
        if (GUILayout.Button("UpIndexes"))
        {
            myScript.UpdateCardsIndexes();
        }
    }
}
