using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGenerator : PileOfCards {

    void Start()
    {
        UpdateCardsIndexes();
    }

    public void UpdateCardsIndexes()
    {
        cards = new List<Card>();

        for (int i = 0; i < transform.childCount; i++)
            cards.Add(transform.GetChild(i).GetComponent<Card>());

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].index = i;
            if (cards[i].startDeckCard)
                CreateNewCard(cards[i]).transform.parent = secondMainPile.transform;
        }

        //foreach (Card card in cards)
        //    card.Flip();
    }

    public Card CreateNewCard(int index)
    {
        return CreateNewCard(cards[index]);
    }

    Card CreateNewCard(Card card)
    {
        return Instantiate(card.gameObject, transform.position, card.transform.rotation).GetComponent<Card>();
        //MoveCardBeetweenPiles(cardCopie, secondMainPile);
    }

    public void AddNewCardToDeck(int index)
    {
        Card card = CreateNewCard(index);
        card.deck = (Deck)secondMainPile;
        card.transform.parent = secondMainPile.transform;
        card.Flip(100);
        MenagersReferencer.GetDeck().InsertCardOnRandomPosition(card);
    }
    
    public void AddRandomCardOfTypeToDeck(Card.Type type)
    {
        List<Card> cardsOfType = new List<Card>();

        foreach (Card card in cards)
            if (card.type == type)
                cardsOfType.Add(card);

        AddNewCardToDeck(cardsOfType[Random.Range(0, cardsOfType.Count)].index);
    }
}
