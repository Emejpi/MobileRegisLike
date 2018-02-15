using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : PileOfCards {

    public float pleacmentRange;
    public float rotationRange;

    bool topJustRemoved;
    bool topJustCreated;

    public int maxDeckSize;

    public CardControl manuCard;

    public Option waitingOption;

    public void TopJustCreated()
    {
        topJustCreated = true;
    }

    public void TopJustRemovedYouAreWelcome()
    {
        topJustRemoved = true;
    }

    public void RemoveTop()
    {
        MenagersReferencer.GetButtonsMenager().Generate(waitingOption);
        MenagersReferencer.GetButtonsMenager().DisActivateButtons();

        Card card = null;

        if (cards.Count > 0)
        {
            if (!topJustRemoved)
            {
                card = cards[0];
            }
        }

        if (((cards.Count == 1 && ! topJustRemoved) || (topJustRemoved && cards.Count == 0)) && (card || (topJustRemoved && !topJustCreated)))
        {

            //MoveCardBeetweenPiles(cards[0], secondMainPile);
            secondMainPile.Remove(maxDeckSize);
       
            Shuffle();
        }

        if (card)
        {
            MoveCardBeetweenPiles(card, secondMainPile);
            card.Flip();
        }

        topJustRemoved = false;
        
        if(!topJustCreated)
            FlipTop();

        topJustCreated = false;
    }

    public void DestroyTop()
    {
        DestroyCard(cards[0]);
    }

    public void GenerateButtons(Card card)
    {
        if (cards.Count == 0 || card != cards[0])
            return;

        MenagersReferencer.GetButtonsMenager().Generate(card.myControl);
    }

    // Use this for initialization
    void Start () {
        topJustCreated = false;
        topJustRemoved = false;
        UpdateCardList();
        Shuffle();
        ApplyRandomPleacment();
        AjustDeckSize(0);
        AddManuCard();
        
        //cards[0].Flip();
    }

    public void AddManuCard()
    {
        Card card = MenagersReferencer.GetCardsGen().CreateNewCard(manuCard);
        //card.Flip(100);
        InsertCard(card, cards.Count);

        for(int i = 0; i < 10; i++)
        MenagersReferencer.GetCardsGen().AddNewCardToDeck(manuCard);
    }

    public void AjustDeckSize(int size)
    {
        while(cards.Count > size)
        {
           cards[0].transform.position = secondMainPile.transform.position;
           FlipTop();
            Card card = cards[0];
           MoveCardBeetweenPiles(card, secondMainPile);
            card.Flip();
           //RemoveTop();
        }
    }
	
    protected void UpdateCardList()
    {
        cards = new List<Card>();

        for (int i = transform.childCount - 1; i >= 0; i--)
            AddCard(transform.GetChild(i).GetComponent<Card>());

        foreach (Card card in cards)
            card.deck = this;
    }

    public void InsertCardOnRandomPosition(Card card)
    {
        InsertCard(card, Random.Range(1, cards.Count + 1));
    }

    public void InsertCard(Card card, int index) //SKONC TO
    {
        AddCard(index, card);
        
        for(int i = index; i >= 0; i--)
        {
            Vector3 pose = cards[i].transform.position;
            cards[i].transform.parent = null;
            cards[i].transform.parent = transform;
            cards[i].transform.position = pose;
        }

        card.Flip(transform.position);
        card.currentPile = this;
        //if(index == 1)
        //{
        //    card.Flip();
        //}
    }

    void PutCardOnDeckTop(Card card)
    {
        cards.Remove(card);
        AddCard(0, card);
        card.transform.parent = null;
        card.transform.parent = transform;
    }

    void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int index = cards.Count - 1;
            Card card = cards[index];
            cards.RemoveAt(index);
            AddCard(0, card);
            Vector3 pose = card.transform.position;
            card.transform.parent = null;
            card.transform.parent = transform;
            card.transform.position = pose;
        }

        for (int i = 0; i < cards.Count; i++)
        {
            int index = Random.Range(0, cards.Count);
            Card card = cards[index];
            cards.RemoveAt(index);
            AddCard(0, card);
            Vector3 pose = card.transform.position;
            card.transform.parent = null;
            card.transform.parent = transform;
            card.transform.position = pose;
        }
    }

    void ApplyRandomPleacment()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.position = transform.position + new Vector3(RandomMP(pleacmentRange), RandomMP(pleacmentRange), i - cards.Count + 1);
            cards[i].transform.eulerAngles = new Vector3(0, 0, Random.Range(0, rotationRange * 2));
        }
    }

    public float RandomMP(float value)
    {
        return Random.Range(-value, value);
    }

    public bool ShuldIChooseDeck()
    {
        return Random.Range(0, 100) < cards.Count * 100 / (cards.Count + secondMainPile.cards.Count);
    }

    //public Card GetRandomCardOfTypeFromCardsInPlay(List<Card.Type> types)
    //{
    //    Card card;

    //    if (ShuldIChooseDeck())
    //    {
    //        if(GetRandomCard(types, out card))
    //        return card;
    //    }
    //    secondMainPile.GetRandomCard(types, out card);

    //    return card;
    //}

    //public Card DestroyCardOfTypeFromCardsInPlay(List<Card.Type> types)
    //{
    //    if(ShuldIChooseDeck())
    //    {
    //        return DestroyCard(types);
    //    }
    //        return secondMainPile.DestroyCard(types);
    //}

}
