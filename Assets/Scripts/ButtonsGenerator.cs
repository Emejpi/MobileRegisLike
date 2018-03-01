using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsGenerator : ChildsReferencer {

    [Header("Settings")]
    public int fontSize;
    public Vector2 iconsCellSize;
    public int iconFontSize;
    public int buttonInsaidCellHigh;

    List<MaxButtonsHolder> rowsHolders;

    [Header("Generator")]
    public int numberOfButtons;

    public Vector2 buttonsSpeace;

    public List<Button> buttons;

    public enum ColorGroup
    {
        basic,
        blocked,
        yes,
        no,
        wrong
    }

    public List<ColorGroup> colorGroups;
    public List<Color> colors;

    Color GetColor( ColorGroup colorGroup)
    {
        for (int i = 0; i < colorGroups.Count; i++)
            if (colorGroups[i] == colorGroup)
                return colors[i];

        return Color.white;
    }

    public void PrepareButtons()
    {
        foreach (Button button in buttons)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(delegate() { MenagersReferencer.GetDeck().RemoveTop(); });
        }
    }

    public void DestroyButtons()
    {
        foreach (MaxButtonsHolder rowsHolder in rowsHolders)
        {
            for (int i = 0; i < rowsHolder.transform.childCount; i++)
            {
                rowsHolder.GetChild(i).GetComponent<MaxButtonsHolder>().currentButtons = 0;
                for (int j = 0; j < rowsHolder.GetChild(i).transform.childCount; j++)
                {
                    rowsHolder.GetChild(i).GetComponent<MaxButtonsHolder>().GetChild(j).SetActive(false);
                }
                rowsHolder.gameObject.SetActive(false);
            }
        }

        PrepareButtons();
    }

    //public void Generate(int numberOfButtons)
    //{
    //    this.numberOfButtons = numberOfButtons;
    //    Generate(); 
    //}

    public void Generate(GraphElement card)
    {
        card.UpdateElements();
        numberOfButtons = card.GetNumberOfElements();

        DestroyButtons();

        if (numberOfButtons == 0)
            return;

        foreach (MaxButtonsHolder rowsHolder in rowsHolders)
        {
            if (rowsHolder.maxButtons >= numberOfButtons)
            {
                rowsHolder.gameObject.SetActive(true);

                bool loopbreaker = false;
                while (!loopbreaker) //you like to play with fire you sick fuck
                {
                    for (int i = 0; i < rowsHolder.transform.childCount; i++)
                    {
                        rowsHolder.GetChild(i).GetComponent<MaxButtonsHolder>().currentButtons++;
                        numberOfButtons--;

                        if (numberOfButtons == 0)
                        {
                            loopbreaker = true;
                            break;
                        }
                    }
                }

                int indexer = 0;
                for (int i = 0; i < rowsHolder.transform.childCount; i++)
                {
                    rowsHolder.GetChild(i).GetComponent<GridLayoutGroup>().cellSize = new Vector2(buttonsSpeace.x / rowsHolder.GetChild(i).GetComponent<MaxButtonsHolder>().currentButtons, rowsHolder.GetChild(i).GetComponent<GridLayoutGroup>().cellSize.y);
                    for (int j = 0; j < rowsHolder.GetChild(i).GetComponent<MaxButtonsHolder>().currentButtons; j++)
                    {
                        Option optionCur = (Option)card.GetElement(indexer);
                        Button buttonCur = rowsHolder.GetChild(i)
                            .GetComponent<MaxButtonsHolder>().GetChild(j).GetComponent<Button>();

                            ModifyButton(buttonCur, optionCur);

                        indexer++;
                    }
                }
                return;
            }
        }
    }

    void ModifyButton(Button button, Option option)
    {
        Option actualOption = option;
        //if(option.randomOption)
        //{
        //    OptionsHolder randomOptionsHolder = option.GetComponent<OptionsHolder>();
        //    if(randomOptionsHolder.options.Count > 0)
        //    {
        //        actualOption = randomOptionsHolder.options[Random.Range(0, randomOptionsHolder.options.Count)];
        //    }
        //}    

        button.GetComponent<SingleButton>().on = true;
        button.gameObject.SetActive(true);
        button.transform.GetChild(0).GetComponent<Text>().text = actualOption.name;

        button.GetComponent<SingleButton>().UpdateInsaid(actualOption);

        if (actualOption.IsAfordable())
        {
            button.GetComponent<Image>().color = GetColor(actualOption.colorGrup);

            //if (actualOption.types.Count > 0)
            {
                //for (int j = 0; j < actualOption.types.Count; j++)
                {
                    button.GetComponent<SingleButton>().SetOnClickEvent();
                }
            }
        }
        else
        {
            button.GetComponent<Image>().color = Color.gray;
            button.onClick.RemoveAllListeners();
            button.GetComponent<SingleButton>().on = false;
        }
    }

    public void DisActivateButtons()
    {
        foreach (MaxButtonsHolder rowsHolder in rowsHolders)
            rowsHolder.gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start () {
        rowsHolders = new List<MaxButtonsHolder>();

        for(int i = 0; i < transform.childCount; i++)
        {
            rowsHolders.Add(GetChild(i).GetComponent<MaxButtonsHolder>());
        }

        buttons = new List<Button>();

        foreach (MaxButtonsHolder rowsHolder in rowsHolders)
        {
            for (int i = 0; i < rowsHolder.transform.childCount; i++)
            {
                for (int j = 0; j < rowsHolder.GetChild(i).transform.childCount; j++)
                {
                    buttons.Add(rowsHolder.GetChild(i).
                        GetComponent<MaxButtonsHolder>().GetChild(j).GetComponent<Button>());
                }
            }
        }

        PrepareButtons();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
