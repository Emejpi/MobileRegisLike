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
        if (GUILayout.Button("Move UP"))
        {
            myScript.transform.position = myScript.transform.parent.position + myScript.transform.parent.childCount * new Vector3(0, 3, 0); 
        }
        if (GUILayout.Button("Flip"))
        {
            myScript.Flip();
        }
        if (GUILayout.Button("Remove"))
        {
            myScript.Flip(new Vector3(10,5,0));
        }
        if (GUILayout.Button("Name"))
        {
            myScript.GenerateTitle();
        }
        if (GUILayout.Button("PrefsUp"))
        {
            //foreach(Option option in myScript.optionsHolder.options)
            ////{
            ////    foreach(Action action in option.actions)
            ////    {
            ////        action.UpdateCardsPrefs();
            ////    }
            //}
            
        }
    }
}
