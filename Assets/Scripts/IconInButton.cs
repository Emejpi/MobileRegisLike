using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconInButton : MonoBehaviour {

    public Image icon;
    public Text text;

	public void UpdateIt(Sprite sprite)
    {
        icon.sprite = sprite;
        //text.text = (value >= 0 ? "+" : "-") + Mathf.Abs(value);
    }

    void Start()
    {
        //text.fontStyle = FontStyle.Bold;
    }
}
