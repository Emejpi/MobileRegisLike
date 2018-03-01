using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : PileOfCards {

    public List<Vector2> chanceSections;

    public float deeperPercent;

    public int currentMaxPriory;
    public int currentMinPriory;

    public int nextPriory;

    public int maxPriory;

    void Start()
    {
        CreateChanceSections(maxPriory);
    }

    public int NextPriory()
    {
        if (chanceSections.Count == 0)
            return 0;

        int chosenNumber = (int)Random.Range(chanceSections[currentMinPriory].x, chanceSections[currentMaxPriory].y);

        for(int i = 0; i < chanceSections.Count; i++)
        {
            Vector2 section = chanceSections[i];
            if(chosenNumber >= section.x && chosenNumber < section.y)
            {
                print("priory: " + i);
                return i;
            }
        }

        return 0;
    }

    void UpdateChanceSections()
    {
        int lastMaxPriory = currentMaxPriory;
        //UpdateMinMaxPriory();

        if(currentMaxPriory != lastMaxPriory)
        {
            CreateChanceSections(currentMaxPriory);
        }
    }

    public void CreateChanceSections(int maxPriory)
    {
        List<float> rowPercents = new List<float>();
        float currentRowPercent = 100;
        float sumOfRowPercents = 0;

        for (int i = 0; i < maxPriory + 1; i++)
        {
            rowPercents.Add(currentRowPercent);
            sumOfRowPercents += currentRowPercent;
            currentRowPercent *= deeperPercent / 100;

            print(currentRowPercent); 
       }

        chanceSections = new List<Vector2>();
        float currentPercent = 0;
        for(int i = 0; i < maxPriory + 1; i++)
        {
            float nextPrecent = currentPercent + rowPercents[maxPriory - i] * 100 / sumOfRowPercents;
            chanceSections.Add(new Vector2((int)currentPercent, (int)nextPrecent));
            currentPercent = nextPrecent;
        }

    }

    void UpdateMinMaxPriory()
    {
        currentMaxPriory = 0;
        currentMinPriory = 100;

        foreach (Card card in cards)
        {
            if (card.myControl.priority > currentMaxPriory)
                currentMaxPriory = card.myControl.priority;

            if (card.myControl.priority < currentMinPriory)
                currentMinPriory = card.myControl.priority;
        }
    }

    int ChosePriory()
    {
        int priory;

        List<int> forbiddenPriorys = new List<int>();

        do
        {
           do
            {
                priory = NextPriory();
            } while (forbiddenPriorys.Contains(priory));

            if (!IsThereCardOfPriory(priory))
            {
                forbiddenPriorys.Add(priory);
                continue;
            }
            break;
        }while(forbiddenPriorys.Count < currentMaxPriory + 1 - currentMinPriory);

        return priory;
    }

    public void Remove(int amount)
    {
        List<Card> cardsInDay = GetCardsAvaibleInDay(MenagersReferencer.GetDeck().NextDay());
        List<Card> allCardsHolder = cards;

        cards = cardsInDay;

        print("all:" + allCardsHolder.Count);

        UpdateMinMaxPriory();

        //print(cardsInDay.Count);

        int acualAmout = amount;

        List<Card> chosenCards = new List<Card>();

        for (int i = cards.Count - 1; i >= 0; i--)
        {
            if (acualAmout-- == 0)
                break;

            Card chosenCard = GetRandomCardOfPriory(ChosePriory());
            chosenCards.Add(chosenCard);
            cards.Remove(chosenCard);

            
        }
        print("all:" + allCardsHolder.Count);
        print("countChosen:" + chosenCards.Count);

        cards = allCardsHolder;

        foreach(Card card in chosenCards)
            MoveCardBeetweenPiles(card, secondMainPile);
    }

}
