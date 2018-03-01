using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsHolder : NamesHolder {

    public enum PointsType
    {
        monay,
        health,
        gardenState,
        curse
    }

    public PointsType type;
    public int value;

    public CardControl cardLow;
    public CardControl cardHigh;

    public GameObject bar;

    public void Add(int value)
    {
        this.value += value;

        this.value = Mathf.Clamp(this.value, 0, 100);

        float scale = this.value / 100f;
        bar.transform.localScale = new Vector3(1, scale, 1);
        //text.text = this.value + "";

        //UpdateNames();
    }

    public void CheckForTooHighTooLow()
    {
        if (this.value == 100)
        {
            MenagersReferencer.GetCardsGen().AddNewCardOnTopOfDeck(cardHigh);
        }
        else if (this.value == 0)
        {
            MenagersReferencer.GetCardsGen().AddNewCardOnTopOfDeck(cardLow);
        }
    }

    //public void Add(int value, string name)
    //{
    //    if (name != "")
    //    {
    //        if (value < 0)
    //        {
    //            names.Remove(name);
    //        }
    //        else if (value > 0)
    //        {
    //            names.Add(name);
    //        }
    //    }
    //    Add(value);
    //}

    //void UpdateNames()
    //{
    //    while(names.Count < value)
    //    {
    //        names.Add(MenagersReferencer.GetCardsGen().namesGen.GetRandomName(type));
    //    }
    //}

    Text text;

    void Start()
    {
        text = transform.GetChild(0).GetComponent<Text>();
        //text.text = value + "";
        Add(0);

        //UpdateNames();
    }
}
