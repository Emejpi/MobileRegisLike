using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour {

    public enum ActionType
    {
        remove,
        add,
        removeThis,
        lose,
        exit
        //cardNext,
        //cardPrevious,
        //hold,
        //repleace
    }
    [Header("Action")]
    public ActionType actionType;

    public int number = 1;

    //public bool useIdent;
    //public List<Card.Type> cardTypes;
   // public Card.Identifaier cardIndent;
    List<Card> cardsPrefs;

    public enum NameAction
    {
        nothing,
        send,
        inharit
    }
    NameAction nameAction;

    public Option option;

    public void UpdateCardsPrefs()
    {
        //cardsPrefs = new List<Card>();
        //if (cardTypes.Count == 0)
        //{
        //    cardsPrefs.Add(MenagersReferencer.GetCardsGen().GetCardWithIdent(cardIndent));
        //}
        //else if(cardIndent == CardStatisctics.Identifaier.noIdent)
        //{
        //    cardsPrefs = MenagersReferencer.GetCardsGen().GetCardsOfType(cardTypes);
        //}
        //else
        //{
        //    cardsPrefs.Add(MenagersReferencer.GetCardsGen().GetCardWithTypeAndIdent(cardTypes, cardIndent));
        //}
    }

    //void AddPrefCard(Card oldCard)
    //{
    //    //if (cardsPrefs.Count == 0)
    //        UpdateCardsPrefs();
    //    Add(option.optionsHolder.card, cardsPrefs[Random.Range(0, cardsPrefs.Count)]);


    //}

    //public void Execute() //Put people back in pleaces. Whould be good if names whould be assigned to your points 
    //{            
    //    for(int i = 0; i < number; i++)
    //        switch (actionType)
    //        {
    //            case ActionType.add:
    //                AddPrefCard(option.optionsHolder.cardCont);
    //                break;

    //            case ActionType.remove:
    //                Remove();
    //                break;

    //            case ActionType.removeThis:
    //                MenagersReferencer.GetDeck().DestroyCard(option.optionsHolder.cardCont);
    //                break;

    //            case ActionType.lose:
    //                MenagersReferencer.GetDeck().DestroyAll();
    //                MenagersReferencer.GetGrave().DestroyAll();
    //                MenagersReferencer.Reload();
    //                //MenagersReferencer.GetDeck().AddManuCard();
    //                break;

    //            case ActionType.exit:
    //                MenagersReferencer.Exit();
    //                break;

    //                //case ActionType.cardNext:
    //                //    Card card1 = GetCardToRemove();
    //                //    Add(card1, card1.cardNext);
    //                //    card1.currentPile.DestroyCard(card1);
    //                //    break;

    //                //case ActionType.cardPrevious:
    //                //    Card card2 = GetCardToRemove();
    //                //    Add(card2, card2.cardPrevious);
    //                //    card2.currentPile.DestroyCard(card2);
    //                //    break;

    //                //case ActionType.repleace:
    //                //    Card card3 = GetCardToRemove();
    //                //    AddPrefCard(card3);
    //                //    card3.currentPile.DestroyCard(card3);
    //                //    break;
    //        }
    //}

   // Card GetCardToRemove()
    //{
        //return MenagersReferencer.GetDeck().GetRandomCardOfTypeFromCardsInPlay(cardTypes);
   // }

    //public string GetTextForDes()
    //{
    //    switch(actionType)
    //    {
    //        case ActionType.add:

    //            break;
    //    }
    //} 

    void Add(Card cardOld, Card cardNew)
    {
        if (!cardNew)
            return;

        //Card card = MenagersReferencer.cardsGen.AddNewCardToDeck(cardNew/*, cardOld*/);

        //switch(nameAction)
        //{
        //    case NameAction.send:
        //        card.name = cardOld.name;
        //        break;

        //    case NameAction.inharit:
        //        for(int i = 0; i < cardOld.namesForText.Count; i ++)
        //        {
        //            foreach(Card.Type type in card.namesTypesForText)
        //            {
        //                if (type == cardOld.namesTypesForText[i])
        //                {
        //                    card.name = cardOld.namesForText[i];
        //                    break;
        //                }
        //            }
        //        }
        //        break;
        //}
        return;

    }

    void Remove()
    {
        //    Card cardToBeDestroyed = MenagersReferencer.GetDeck().GetCardWithTypeAndIdent(cardTypes, cardIndent);
        //if(cardToBeDestroyed == new Card())
        //{
        //    cardToBeDestroyed = MenagersReferencer.GetGrave().GetCardWithTypeAndIdent(cardTypes, cardIndent);
        //    if (cardToBeDestroyed != new Card())
        //    {
        //        MenagersReferencer.GetGrave().DestroyCard(cardToBeDestroyed);
        //    }
        //}
        //else
        //{
        //    MenagersReferencer.GetDeck().DestroyCard(cardToBeDestroyed);
        //}

    }

    void Start()
    {
        //UpdateCardsPrefs();
        enabled = false;
    }
}
