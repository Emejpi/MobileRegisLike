using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileOfCards : MenagingObject {

    public List<Card> cards;

    public PileOfCards secondMainPile;

    public Card GetRandomCard(int index)
    {
        List<Card> cardsOfIndex = new List<Card>();

        foreach (Card card in cards)
            if (card.index == index)
                cardsOfIndex.Add(card);

        if (cardsOfIndex.Count == 0)
            return new Card();

        return cardsOfIndex[Random.Range(0, cardsOfIndex.Count)];
    }

    //public List<Card> GetCardsOfType(List<Card.Type> types)
    //{
    //    List<Card> cardsOfType = new List<Card>();

    //    foreach (Card card in cards)
    //        if (card.IsItOfTypes(types))
    //            cardsOfType.Add(card);

    //    return cardsOfType;
    //}

    //public bool GetRandomCard(List<Card.Type> types, out Card outCard)
    //{
    //    List<Card> cardsOfType = GetCardsOfType(types);

    //    if (cardsOfType.Count == 0)
    //    {
    //        outCard = new Card();
    //        return false;
    //    }

    //    outCard = cardsOfType[Random.Range(0, cardsOfType.Count)];
    //    return true;
    //}

    public void FlipAll()
    {
        foreach (Card card in cards)
            card.Flip();
    }

    public void RemoveAll()
    {
        for(int i = cards.Count - 1; i >= 0; i--)
            MoveCardBeetweenPiles(cards[i], secondMainPile);
    }

    public void Remove(int amount)
    {
        int acualAmout = amount;

        for (int i = cards.Count - 1; i >= 0; i--)
        {
            if (acualAmout-- == 0)
                return;
            MoveCardBeetweenPiles(cards[Random.Range(0,cards.Count)], secondMainPile);
        }
    }

    public void FlipTop()
    {
        cards[0].Flip();
    }

    public void RemoveTop()
    {
        MoveCardBeetweenPiles(cards[0], secondMainPile);
    }

    public void AddCard(Card card)
    {
        AddCard(cards.Count, card);
    }

    public void AddCard(int index, Card card)
    {
        cards.Insert(index, card);
        card.currentPile = this;
    }

    //public Card GetCardWithIdent(Card.Identifaier ident)
    //{
    //    foreach (Card card in cards)
    //    {
    //        if (card.ident == ident)
    //            return card;
    //    }

    //    return new Card();
    //}

    //public bool IsThereCardWithTypeAndIdent(List<Card.Type> cardTypesNeeded, Card.Identifaier identNeeded)
    //{
    //    return GetCardWithTypeAndIdent(cardTypesNeeded, identNeeded) != new Card();
    //}

    //public Card GetCardWithTypeAndIdent(List<Card.Type> cardTypesNeeded, Card.Identifaier identNeeded)
    //{
    //    foreach (Card card in cards)
    //    {
    //        if ((card.IsItOfTypes(cardTypesNeeded))
    //            && (identNeeded == CardStatisctics.Identifaier.noIdent || card.ident == identNeeded))
    //            return card;
    //    }
    //    return new Card();
    //}

    public void DestroyAll()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            cards[i].Flip(new Vector3(0, 100, 0));
            //cards.RemoveAt(0);
        }
    }

    public Card DestroyCard(Card card)
    {
        //List<Card> cards = GetCardsFromDeckAndGrave();
        card.Flip(MenagersReferencer.GetCardsGen().transform.position);
        card.DestroyIt();

        if (card == MenagersReferencer.GetDeck().cards[0])
            MenagersReferencer.GetDeck().TopJustRemovedYouAreWelcome();

        cards.Remove(card);

        return card;
    }

    //public Card DestroyCard(List<Card.Type> types)
    //{
    //    Card cardToDestroy;
    //    if(GetRandomCard(types, out cardToDestroy))
    //        return DestroyCard(cardToDestroy);

    //    return new Card();
    //}

    protected void MoveCardBeetweenPiles(Card card, PileOfCards pileTo)
    {
        pileTo.AddCard(card);
        card.Flip(pileTo.transform.position);
        card.currentPile = pileTo;
        cards.Remove(card);
    }

}
