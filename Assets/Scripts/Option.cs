using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : GraphElement {

    [Header("Option")]
    public ButtonsGenerator.ColorGroup colorGrup;
    public string text = "";

    public List<PointsHolder.PointsType> types;
    public List<int> values;

    [System.Serializable]
    public class PriorityModifaier
    {
        public int value;
        public GraphElement element;
    }

    public PriorityModifaier priorityMod;
    public List<PriorityModifaier> priorityMods;

    public bool removeIt;
    public bool showNewCardRightAway;


    bool descriptionGenerated;

    //void GenerateDescription()
    //{
    //    if (descriptionGenerated)
    //        return;

    //    string text =  " ";

    //    foreach (Action action in actions)
    //    {
    //        switch(action.actionType)
    //        {
    //            case Action.ActionType.add:
    //                text += AddText(action);
    //                break;

    //            case Action.ActionType.remove:
    //                text += "Removes ";
    //                if (action.cardIndent == CardStatisctics.Identifaier.noIdent)
    //                    text += "random ";
    //                foreach (Card.Type type in action.cardTypes)
    //                    text += type.ToString().ToUpper() + " ";
    //                if (action.cardIndent != CardStatisctics.Identifaier.noIdent)
    //                    text += optionsHolder.card.GetStringFromIdent(action.cardIndent) + " ";
    //                text += "from your DECK.";
    //                break;

    //            case Action.ActionType.removeThis:
    //                text += "Removes THIS CARD from your DECK.";
    //                break;

    //            //case ActionType.repleace:
    //            //    text += RemoveText(action);
    //            //    text += "\n";
    //            //    text += AddText(action);

    //            //    break;

    //            //case ActionType.cardNext:
    //            //    text += RemoveText(action);
    //            //    text += "\n";
    //            //    text += "And adds it's next form to your deck.";
    //            //    break;

    //            //case ActionType.cardPrevious:
    //            //    text += RemoveText(action);
    //            //    text += "\n";
    //            //    text += "And adds it's previous form to your deck.";
    //            //    break;
    //        }

    //        text += "\n \n";
    //    }

    //    for (int i = 0; i < types.Count; i++)
    //    {
    //        if (values[i] > 0)
    //            text += "+";
    //        else
    //            text += "-";

    //        text += values[i] + " " + types[i].ToString().ToUpper();

    //        text += "\n \n";
    //    }

        //text += " \n";

    //    GetComponent<Text>().text += text;

    //    descriptionGenerated = true;
    //}

    //string AddText(Action action)
    //{
    //    string text = "Adds ";

    //    if(action.number > 1)
    //    {
    //        text += action.number + "x ";
    //    }

    //    if (action.cardTypes.Count == 0)
    //    {
    //        //text += MenagersReferencer.GetCardsGen().GetCardWithIdent(action.cardIndent).name + " ";
    //        text += optionsHolder.card.GetStringFromIdent(action.cardIndent) + " ";
    //    }
    //    else if(action.cardIndent == CardStatisctics.Identifaier.noIdent)
    //    {
    //        text += "random ";
    //        foreach (Card.Type type in action.cardTypes)
    //            text += type.ToString().ToUpper() + " ";
    //    }
    //    else
    //    {
    //        for(int i = 0; i < action.cardTypes.Count; i++)
    //            text += action.cardTypes[i].ToString().ToUpper() + " ";
    //        text += optionsHolder.card.GetStringFromIdent(action.cardIndent) + " ";
    //    }

    //    text += "to your DECK.";

    //    return text;
    //}

    //string RemoveText(Action action)
    //{
    //    string text = "Removes ";

    //    //if (action.useIdent)
    //    //{
    //    //    text += "this card";
    //    //}
    //    //else
    //    //{
    //    //    text += "random ";
    //    //    foreach (Card.Type type in action.cardTypes)
    //    //        text += type.ToString() + " ";
    //    //}

    //    text += " from your deck.";

    //    return text;
    //}

    public void ShowDescription()
    {
        MenagersReferencer.GetDeck().cards[0].ShowDescription(GetComponent<Text>());
    }

    public bool IsAfordable()
    {
        if (this == new Option(ButtonsGenerator.ColorGroup.blocked, "..."))
            return true;

        //if(identNeeded != CardStatisctics.Identifaier.noIdent || cardTypesNeeded.Count > 0)
        //if (!MenagersReferencer.GetDeck().IsThereCardWithTypeAndIdent(cardTypesNeeded, identNeeded) 
        //    && !MenagersReferencer.GetGrave().IsThereCardWithTypeAndIdent(cardTypesNeeded, identNeeded))
        //    return false;

        for (int i = 0; i < types.Count; i++)
        {
            if (MenagersReferencer.pointsMenager.GetValue(types[i]) < -values[i])
                return false;
        }

        return true;
    }

    public void ExecuteOption()
    {
        for (int j = 0; j < types.Count; j++)
        {
            MenagersReferencer.pointsMenager.AddPoints(values[j], types[j]/*optionsHolder.card.GetNameOfType(types[j])*/);
        }

        if(removeIt)
        {
            MenagersReferencer.GetDeck().DestroyTop();
        }

        UpdateElements();

        for(int i = 0; i < GetNumberOfElements(); i++) //FINISH IT
        {
            CardControl cardCont = (CardControl)GetElement(i);

            if(showNewCardRightAway)
            {
                MenagersReferencer.GetCardsGen().AddNewCardOnTopOfDeck(cardCont);
            }
            else
                MenagersReferencer.GetCardsGen().AddNewCardToDeck(cardCont);
        }

        foreach(PriorityModifaier mod in priorityMods)
        {
            mod.element.priority += mod.value;
        }

    }

    public Option(ButtonsGenerator.ColorGroup color, string text)
    {
        types = new List<PointsHolder.PointsType>();
        values = new List<int>();

        this.colorGrup = color;
        this.text = text;
    }

    void Start()
    {
        Prepare();

        if (priorityMod.element != null)
            priorityMods.Add(priorityMod);
    }

}
