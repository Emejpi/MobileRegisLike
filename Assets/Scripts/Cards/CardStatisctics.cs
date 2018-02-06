using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardStatisctics : BasicActions {
    [Header("Design Speace")]
    [Tooltip("? - new name, \n ident - the same as ident, \n blank - inharited")]
    string name;

    string tittleTextBase;

    public bool startDeckCard;

    public enum Type
    {
        Regular,
        Room,
        Person,
        Monster,
        Feeding,
        Scientist,
        Monay,
        Knowladge,
        nervous,
        furious
    }

    public enum Identifaier
    {
        noIdent,
        scientist,
        scientist_teaching,
        volunteers,
        people_eater,
        attentionster,
        laboratory,
        builder,
        mind_eater,
        nursing_room,
        anxiety,
        bar,
        sidekick,
        manu
    }

    public Identifaier ident;

    public List<Type> types;

    public bool addTypesToTittle;
    public int startIndexForAddingTexts;

    public string mainText;

    Card cardNext;
    Card cardPrevious;

    [Tooltip("? - new name, \n name - the same as name, \n blank - inharited")]
    List<string> namesForText;
    List<Card.Type> namesTypesForText;

    public bool IsItOfTypes(List<Type> types)
    {
        //if (types.Count == 0)
        //    return true;
        foreach (Type type in types)
            if (!IsItOfType(type))
                return false;
        return true;
    }

    public bool IsItOfType(Type type)
    {
        foreach (Type typeIter in types)
            if (typeIter == type)
                return true;
        return false;
    }

    [Header("Rest")]
    public string tittleText;
    public Text tittle;
    public Text typeText;
    public TextWithSpecialSymbols textMain;

    public OptionsHolder optionsHolder;

    public string GetTittle(string name)
    {
        return tittleTextBase.Replace("name?", name);
    }

    public string GetStringFromIdent(Identifaier id)
    {
        return id.ToString().ToUpper().Replace("_", " ");
    }

    //public string GetNameOfType(Card.Type type)
    //{
    //    for(int i = 0; i < namesForText.Count; i++)
    //    {
    //        if(namesTypesForText[i] == type)
    //        {
    //            return namesForText[i];
    //        }
    //    }

    //    return "";
    //}

    public void GenerateTitle()
    {
        tittle.text = "";

        if (addTypesToTittle)
        {
            for (int i = startIndexForAddingTexts; i < types.Count; i++)
                tittle.text += types[i].ToString().ToUpper() + " ";
        }

        tittle.text += GetStringFromIdent(ident);
        InsertNamesInTexts("tittle?", tittle.text);

        if (types.Count > 0)
            typeText.text = types[0].ToString().ToUpper();
        else
            typeText.transform.parent.gameObject.SetActive(false);

        //if (tittleText == "")
        //{

            //    tittleText = "";

            //    if (name == "ident")
            //    {
            //        name = ident.ToString();
            //    }
            //    else if (name == "")
            //    {
            //        for (int i = 0; i < namesForText.Count; i++)
            //            if (IsItOfType(namesTypesForText[i]))
            //            {
            //                name = namesForText[i];
            //                break;
            //            }

            //    }
            //    else if (name == "?")
            //    {
            //        name = MenagersReferencer.GetCardsGen().namesGen.GetRandomName(types[0]);
            //    }

            //    //for(int i = types.Count - 1; i >= 0; i--)
            //    //{
            //    //    tittleText += MenagersReferencer.GetCardsGen().namesGen.GetNameOfType(types[i]);
            //    //    tittleText += " ";
            //    //}

            //    tittleText += GetTittle(name);
            //}
            //    tittle.text = tittleText;

            //GenerateNames();

    }

    void GenerateNames()
    {
        for (int i = 0; i < namesForText.Count; i++)
        {
            if (namesForText[i] == "name")
            {
                namesForText[i] = name;
            }
            else if (namesForText[i] == "?")
            {
                namesForText[i] = MenagersReferencer.GetPointsMenager().GetPointsHolder(namesTypesForText[i]).TestRandomName(); 
            }
        }

        //InsertNamesInTexts();
    }

    void InsertNamesInTexts(string oldText, string newText)
    {
        //for (int i = 0; i < namesForText.Count; i++)
        //{
            textMain.text = textMain.text.Replace(oldText, newText);
            //textMain.text = textMain.text.Replace("name?", name);
            //textMain.text =  textMain.text.Replace((namesTypesForText[i]).ToString() + "?", namesForText[i]);

            foreach (Option option in optionsHolder.options)
            {
                //print((namesTypesForText[i]).ToString() + "?");
                //if(option.GetComponent<TextWithSpecialSymbols>().specialSymbols)
                {
                    option.text = option.text.Replace(oldText, newText);
            }
            }
      //  }
    }
}
