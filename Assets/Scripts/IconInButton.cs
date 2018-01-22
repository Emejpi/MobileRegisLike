using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconInButton : MonoBehaviour {

    public Image icon;
    public Text text;

	public void UpdateIt(Sprite sprite, float value)
    {
        icon.sprite = sprite;
        text.text = (value >= 0 ? "+" : "-") + Mathf.Abs(value);
    }
}
