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
    public void Generate(Option option)
    {
         Generate(new OptionsHolder(option));        
    }

    public void Generate(OptionsHolder optionsHolder)
    {
        numberOfButtons = optionsHolder.GetNumberOfOptions();

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
                        ModifyButton(rowsHolder.GetChild(i)
                            .GetComponent<MaxButtonsHolder>().GetChild(j).GetComponent<Button>(),
                            optionsHolder.GetOption(indexer));

                        indexer++;
                    }
                }
                return;
            }
        }
    }

    void ModifyButton(Button button, Option option)
    {
        button.gameObject.SetActive(true);
        button.transform.GetChild(0).GetComponent<Text>().text = option.text;

        button.GetComponent<SingleButton>().UpdateInsaid(option);

        if (IsAfordable(option))
        {
            button.GetComponent<Image>().color = option.color;

            if (option.types.Count > 0)
            {
                for (int j = 0; j < option.types.Count; j++)
                {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(delegate ()
                    {
                        option.ExecuteOption();
                    });
                    button.onClick.AddListener(delegate () { MenagersReferencer.GetDeck().RemoveTop(); });
                }
            }
        }
        else
        {
            button.GetComponent<Image>().color = Color.gray;
            button.onClick.RemoveAllListeners();
        }
    }

    bool IsAfordable(Option option)
    {
        if (option.types.Count == 0)
            return true;

        for(int i = 0; i < option.types.Count; i++)
        {
            if (MenagersReferencer.pointsMenager.GetValue(option.types[i]) < - option.values[i])
                return false;
        }

        return true;
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
