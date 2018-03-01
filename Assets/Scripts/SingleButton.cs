using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleButton : MonoBehaviour {

    public GameObject iconsHolder;
    public Text text;

    public Option myOption;

    bool descriptionTimer;
    float timer;
    public float timerDur;

    public bool on;

    public void DisactivateDescriptionTimer()
    {
        descriptionTimer = false;
    }

    public void ActivateDescriptionTimer()
    {
        if (!on)
            return;

        descriptionTimer = true;
        timer = Time.time + timerDur;
    }

    void Update()
    {
        if(descriptionTimer && timer < Time.time)
        {
            ShowDescription();
        }
    }

    void ShowDescription()
    {
        if (!on)
            return;

        descriptionTimer = false;

        Button button = GetComponent<Button>();

        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(delegate () { SetOnClickEvent(); });

        myOption.ShowDescription();
    }

    void HideDescription()
    {
        if (MenagersReferencer.GetDeck().cards.Count > 0)
            MenagersReferencer.GetDeck().cards[0].HideDescription();
    }

    public void SetOnClickEvent()
    {
        if (!on)
            return;

        DisactivateDescriptionTimer();
        HideDescription();

        Button button = GetComponent<Button>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate ()
        {
            myOption.ExecuteOption();
        });
        button.onClick.AddListener(delegate () { DisactivateDescriptionTimer(); });
        button.onClick.AddListener(delegate () { MenagersReferencer.GetDeck().RemoveTop(); });
    }

    public void UpdateInsaid(Option option)
    {
        myOption = option;

        GetComponent<GridLayoutGroup>().cellSize = new Vector2(transform.parent.GetComponent<GridLayoutGroup>().cellSize.x - 10, transform.parent.GetComponent<GridLayoutGroup>().cellSize.y);


        int i = 0;
        for (i = 0; i < iconsHolder.transform.childCount; i++)
        {
            if (i >= option.types.Count)
            {
                if (option.unlockableSCond.Count != 0 && option.unlockableSCond[0].needed)
                {
                    iconsHolder.transform.GetChild(i).gameObject.SetActive(true);

                    iconsHolder.transform.GetChild(i).GetComponent<IconInButton>().
                        UpdateIt(MenagersReferencer.GetUnlockablesManager().GetUnlocaBleSprite(option.unlockableSCond[0].name));

                    i++;
                }
                break;
            }
            else
            {
                iconsHolder.transform.GetChild(i).gameObject.SetActive(true);

                iconsHolder.transform.GetChild(i).GetComponent<IconInButton>().
                    UpdateIt(MenagersReferencer.GetPointsMenager().GetPointsHolder(option.types[i]).GetComponent<Image>().sprite);
            }
            }

        if(option.types.Count == 0 && (option.unlockableSCond.Count == 0 || !option.unlockableSCond[0].needed))
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).gameObject.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            GetComponent<GridLayoutGroup>().cellSize = new Vector2(transform.parent.GetComponent<GridLayoutGroup>().cellSize.x - 10, transform.parent.GetComponent<GridLayoutGroup>().cellSize.y);

        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.GetComponent<Text>().alignment = TextAnchor.LowerCenter;
            GetComponent<GridLayoutGroup>().cellSize = new Vector2(transform.parent.GetComponent<GridLayoutGroup>().cellSize.x - 10, transform.parent.GetComponent<GridLayoutGroup>().cellSize.y / 2);

        }

        for (; i < iconsHolder.transform.childCount; i++)
        {
            iconsHolder.transform.GetChild(i).gameObject.SetActive(false);
        }

        //if(option)
        //if (option.actions.Count > 0)
        //    iconsHolder.transform.GetChild(iconsHolder.transform.childCount - 1).gameObject.SetActive(true);
    }

}
