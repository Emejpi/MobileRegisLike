using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsHolder : NamesHolder {

    public enum PointsType
    {
        monay,
        people,
        rooms,
        knowladge
    }

    public PointsType type;
    public int value;

    public void Add(int value)
    {
        this.value += value;

        if (this.value < 0)
            this.value = 0;

        text.text = this.value + "";

        //UpdateNames();
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
        text.text = value + "";

        //UpdateNames();
    }
}
