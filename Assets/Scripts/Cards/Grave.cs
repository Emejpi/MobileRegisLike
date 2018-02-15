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
                return i;
            }
        }

        return 0;
    }

    void UpdateChanceSections()
    {
        int lastMaxPriory = currentMaxPriory;
        UpdateMinMaxPriory();

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

    public void Remove(int amount)
    {
        int acualAmout = amount;

        for (int i = cards.Count - 1; i >= 0; i--)
        {
            if (acualAmout-- == 0)
                return;
            MoveCardBeetweenPiles(cards[Random.Range(0, cards.Count)], secondMainPile);
        }
    }

}
