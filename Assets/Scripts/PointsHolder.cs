using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsHolder : MonoBehaviour {

    public enum Type
    {
        people,
        monay,
        rooms
    }

    public void Add(int value)
    {
        this.value += value;

        if (this.value < 0)
            this.value = 0;

        text.text = this.value + "";
    }

    public Type type;
    public int value;

    Text text;

    void Start()
    {
        text = transform.GetChild(0).GetComponent<Text>();
        text.text = value + "";
    }
}
