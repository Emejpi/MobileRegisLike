using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControl : GraphElement {

    int index;

    public CardControl inharited;

    public string text;

    public bool startDeck;

    void Start()
    {
        Prepare();
        index = MenagersReferencer.GetCardsGen().GetNewIndex();
    }

}
