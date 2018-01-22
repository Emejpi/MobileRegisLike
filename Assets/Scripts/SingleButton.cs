using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleButton : MonoBehaviour {

    public GameObject iconsHolder;
    public Text text;

    public void UpdateInsaid(Option option)
    {
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(transform.parent.GetComponent<GridLayoutGroup>().cellSize.x, GetComponent<GridLayoutGroup>().cellSize.y);

        int i = 0;
        for (i = 0; i < iconsHolder.transform.childCount; i++)
        {
            if (i >= option.types.Count)
                break;

            iconsHolder.transform.GetChild(i).gameObject.SetActive(true);

            iconsHolder.transform.GetChild(i).GetComponent<IconInButton>().
                UpdateIt(MenagersReferencer.pointsMenager.GetPointsHolder(
                    option.types[i]).GetComponent<Image>().sprite, option.values[i]);
        }

        for (; i < iconsHolder.transform.childCount; i++)
        {
            iconsHolder.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
