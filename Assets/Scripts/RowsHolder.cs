using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowsHolder : MaxButtonsHolder {

    public float rowsHolderMultiplayer = 1;

    public List<SingleButton> buttons;

    ButtonsGenerator buttonsGen;

    void Start()
    {
        buttons = new List<SingleButton>();
        buttonsGen = transform.parent.GetComponent<ButtonsGenerator>();

        for(int i = 0; i < transform.childCount; i++)
        {
            MaxButtonsHolder row = transform.GetChild(i).GetComponent<MaxButtonsHolder>();
            for(int j = 0; j < row.transform.childCount; j++)
            {
                buttons.Add(row.transform.GetChild(j).GetComponent<SingleButton>());
            }
        }

        foreach(SingleButton button in buttons)
        {
            button.text.fontSize = (int)(buttonsGen.fontSize * rowsHolderMultiplayer);
            button.iconsHolder.GetComponent<GridLayoutGroup>().cellSize = buttonsGen.iconsCellSize * rowsHolderMultiplayer;
            button.GetComponent<GridLayoutGroup>().cellSize = new Vector2(button.transform.parent.GetComponent<GridLayoutGroup>().cellSize.x, button.transform.parent.GetComponent<GridLayoutGroup>().cellSize.y / 2);

            for (int i = 0; i < button.iconsHolder.transform.childCount; i++)
            {
                IconInButton icon = button.iconsHolder.transform.GetChild(i).GetComponent<IconInButton>();
                //icon.text.fontSize = (int)(buttonsGen.iconFontSize * rowsHolderMultiplayer);
                icon.GetComponent<GridLayoutGroup>().cellSize = new Vector2(buttonsGen.iconsCellSize.x, buttonsGen.iconsCellSize.y) * rowsHolderMultiplayer;

            }
        }
    }
}
