using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour {

    public Color color = Color.white;
    public string text = "";

    public List<PointsHolder.Type> types;
    public List<int> values;

    public List<int> cardsToCreateIndexes;

    public void ExecuteOption()
    {
        for (int j = 0; j < types.Count; j++)
        {
            MenagersReferencer.pointsMenager.AddPoints(values[j], types[j]);
        }

        foreach(int i in cardsToCreateIndexes)
        {
            MenagersReferencer.cardsGen.AddNewCardToDeck(i);
        }
    }

    public Option(Color color, string text)
    {
        types = new List<PointsHolder.Type>();
        values = new List<int>();

        this.color = color;
        this.text = text;
    }

}
