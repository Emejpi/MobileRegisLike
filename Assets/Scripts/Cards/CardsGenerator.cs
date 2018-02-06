using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGenerator : PileOfCards {

    public NamesGenerator namesGen;

    void Start()
    {
        //print(Card.Type.monster.ToString());

        UpdateCardsIndexes();
        GenerateStartDeck();
    }

    public void UpdateCardsIndexes()
    {
        cards = new List<Card>();

        for (int i = 0; i < transform.childCount; i++)
        {
            AddCard(transform.GetChild(i).GetComponent<Card>());
            cards[cards.Count - 1].gameObject.SetActive(false);
        }

        //foreach (Card card in cards)
        //    card.Flip();
    }

    public void GenerateStartDeck()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].index = i;
            if (cards[i].startDeckCard)
                CreateNewCard(cards[i]).transform.parent = secondMainPile.transform;
        }
    }

    Card CreateNewCard(int index)
    {
        return CreateNewCard(cards[index]);
    }

    public Card CreateNewCard(Card card)
    {
        GameObject newCard = Instantiate(card.gameObject, transform.position, card.transform.rotation);
        newCard.SetActive(true);
        return newCard.GetComponent<Card>();
        //MoveCardBeetweenPiles(cardCopie, secondMainPile);
    }

    public Card AddNewCardToDeck(Card cardPref /*, Card iniciator*/)
    {
        Card card = CreateNewCard(cardPref);
        card.deck = (Deck)secondMainPile;
        card.transform.parent = secondMainPile.transform;

        //for (int i = 0; i < iniciator.namesForText.Count; i++)
        //    card.AddNameForText(iniciator.namesForText[i], iniciator.namesTypesForText[i]);

        if (false && MenagersReferencer.GetDeck().ShuldIChooseDeck())
        {
            card.Flip(100);
            MenagersReferencer.GetDeck().InsertCardOnRandomPosition(card);
        }
        else
        {
            MoveCardBeetweenPiles(card, MenagersReferencer.GetGrave());
        }

        return card;
    }
    
    public void AddCardRandomToDeck(List<Card.Type> types/*, Card iniciator*/)
    {
        Card cardToAdd;
        if(GetRandomCard(types, out cardToAdd))
            AddNewCardToDeck(cardToAdd/*, iniciator*/);
    }

    public void AddCardOfIdentToDeck(Card.Identifaier ident)
    {
        Card cardToAdd = GetCardWithIdent(ident);
            AddNewCardToDeck(cardToAdd);
    }
}
