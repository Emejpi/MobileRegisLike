using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileOfCards : MenagingObject {

    public List<Card> cards;

    public PileOfCards secondMainPile;

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

    public void FlipTop()
    {
        cards[0].Flip();
    }

    public void RemoveTop()
    {
        MoveCardBeetweenPiles(cards[0], secondMainPile);
    }

    protected void MoveCardBeetweenPiles(Card card, PileOfCards pileTo)
    {
        pileTo.cards.Add(card);
        card.Flip(pileTo.transform.position);
        cards.Remove(card);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
