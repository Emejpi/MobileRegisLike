using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : PileOfCards {

    public float pleacmentRange;
    public float rotationRange;

    public void RemoveTop()
    {
        MenagersReferencer.GetButtonsMenager().Generate(new Option(Color.gray, "WAIT"));
        MenagersReferencer.GetButtonsMenager().DisActivateButtons();

        if (cards.Count > 1)
        {
            Card card = cards[0];
            MoveCardBeetweenPiles(cards[0], secondMainPile);
            card.Flip();
        }
        else if (cards.Count == 1)
        {
            cards[0].Flip();
            //MoveCardBeetweenPiles(cards[0], secondMainPile);
            secondMainPile.RemoveAll();

            Shuffle();
        }

        FlipTop();
    }

    public void GenerateButtons(Card card)
    {
        if (card != cards[0])
            return;

        MenagersReferencer.GetButtonsMenager().Generate(card.optionsHolder);
    }

    // Use this for initialization
    void Start () {
        UpdateCardList();
        Shuffle();
        ApplyRandomPleacment();
        cards[0].Flip();
    }
	
    protected void UpdateCardList()
    {
        cards = new List<Card>();

        for (int i = transform.childCount - 1; i >= 0; i--)
            cards.Add(transform.GetChild(i).GetComponent<Card>());

        foreach (Card card in cards)
            card.deck = this;
    }

    public void InsertCardOnRandomPosition(Card card)
    {
        InsertCard(card, Random.Range(1, cards.Count + 1));
    }

    void InsertCard(Card card, int index) //SKONC TO
    {
        cards.Insert(index, card);
        
        for(int i = index; i >= 0; i--)
        {
            Vector3 pose = cards[i].transform.position;
            cards[i].transform.parent = null;
            cards[i].transform.parent = transform;
            cards[i].transform.position = pose;
        }

        card.Flip(transform.position);
        //if(index == 1)
        //{
        //    card.Flip();
        //}
    }

    void PutCardOnDeckTop(Card card)
    {
        cards.Remove(card);
        cards.Insert(0, card);
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
            cards.Insert(0, card);
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
            cards.Insert(0, card);
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

    public void ChangeCard(Card card, int index)//FINISH IT
    {

    }

}
