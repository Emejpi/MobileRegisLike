using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : CardStatisctics {

    public CardControl myControl;

    public int index;

    public PileOfCards currentPile;

    Vector3 targetRot = new Vector3(0, 180, 10);

    public enum State
    {
        nothing,
        flipping,
        removeing
    }
    [Header("Movement")]
    public State state;

    public float speedFlipping;
    public float scaleFlipping = 1;
    public float sideMoveFlipping = 1;
    public float poseChangingSpeedFlipping = 10;

    float extraSpeed;

    Vector3 startPose;
    Vector3 targetPose;

    public List<Vector3> flipsQueaue;
    bool addFlippingToQueue;

    public Deck deck;

    int currentRightRot;

    public delegate void OnFlipped();
    public OnFlipped onFlipped;

    public Text descriptionText;

    public Card cardHolded;

    //public void AddNameForText(string name, Card.Type type)
    //{

    //    for(int i = 0; i < namesForText.Count; i++)
    //    {
    //        if(namesTypesForText[i] == type)
    //        {
    //            if(namesForText[i] == "")
    //            {
    //                  namesForText[i] = name;
    //            }
    //        }
    //    }

    //    //namesForText.Add(name);
    //    //namesTypesForText.Add(type);
    //}

    public void InsertNamesToText()
    {
        Text text;
        //text.text.Replace()
    }

    public void ShowDescription(Text text)
    {
        descriptionText.transform.parent.gameObject.SetActive(true);
        descriptionText.text = text.text;
    } 

    public void HideDescription()
    {
        descriptionText.transform.parent.gameObject.SetActive(false);
    }

    void Start()
    {
        //textMain.text = mainText;
        //GenerateTitle();
            //addFlippingToQueue = false;

            //currentRightRot = 0;
    }

    public void DestroyIt()
    {
        Flip(new Vector3(6, 6, 6));
    }

    public void Flip(float extraSpeed)
    {
        this.extraSpeed = extraSpeed;
        Flip();
    }

    public void Flip()
    {
        ActualFlip();
        if (addFlippingToQueue)
        {
            if (flipsQueaue.Count > 0)
                FlippingCheck(flipsQueaue[flipsQueaue.Count - 1]);
            else
                FlippingCheck(targetPose);
        }
        else
        {
            FlippingCheck(startPose);
        }
    }
    
    public void Flip(Vector3 targetPose)
    {
        ActualFlip();
        FlippingCheck(targetPose);
    }

    void Fliped()
    {
        //if (MenagersReferencer.GetDeck().cards.Count > 0 && MenagersReferencer.GetDeck().cards[0] == this)
        //    GenerateTitle();

        extraSpeed = 0;

        if(currentRightRot == 180)
            MenagersReferencer.GetDeck().GenerateButtons(this);
    }

    void ActualFlip()
    {
        if(flipsQueaue.Count == 0)
        {
            flipsQueaue = new List<Vector3>();
        }
        if (state == State.flipping)
            addFlippingToQueue = true;
        else
        {

            state = State.flipping;
            startPose = transform.position;
            currentRightRot = 180 - currentRightRot;
            targetRot = new Vector3(0, currentRightRot, 
                10 + (currentRightRot == 180 ? 0 : MenagersReferencer.GetDeck().RandomMP(MenagersReferencer.GetDeck().rotationRange)));
        }
    }

    void FlippingCheck(Vector3 targetPose)
    {
        if(addFlippingToQueue)
        {
            flipsQueaue.Add(targetPose);
            addFlippingToQueue = false;
        }
        else
        {
            this.targetPose = targetPose;
        }
    }

    Vector3 GetRandomizedPose()
    {
        deck = MenagersReferencer.GetDeck();
        return new Vector3(deck.RandomMP(deck.pleacmentRange), deck.RandomMP(deck.rotationRange), 0);

    }

    void Update()
    {
        switch (state)
        {
            case State.flipping:
                if (Mathf.Abs(transform.eulerAngles.z - targetRot.z) > Mathf.Abs(transform.eulerAngles.y - targetRot.y))
                    transform.eulerAngles = LerpVec3(transform.eulerAngles, targetRot, (speedFlipping + extraSpeed) * 4 * Time.deltaTime);
                else
                    transform.eulerAngles = Vector3.RotateTowards(transform.eulerAngles,
                        new Vector3(targetRot.x, targetRot.y, targetRot.z),
                        (speedFlipping + extraSpeed)* Time.deltaTime, (speedFlipping + extraSpeed) * Time.deltaTime * 75);

                float parabol = (90 - Mathf.Abs(transform.eulerAngles.y - 90)) / 90;
                float currentScale = 0.05f + parabol * scaleFlipping;
                float curretSideMove = parabol * sideMoveFlipping / (10 / poseChangingSpeedFlipping);

                transform.localScale = new Vector3(currentScale, currentScale, 0);
                transform.position = Vector3.MoveTowards(transform.position, targetPose, poseChangingSpeedFlipping * Time.deltaTime)
                    + new Vector3(curretSideMove, curretSideMove, 0);

                if (CloseEnought(targetRot.y, transform.eulerAngles.y, 1f)
                    && Vector3.Distance(transform.position, targetPose) < 0.1f)
                {
                    Fliped();
                    state = State.nothing;
                }

                break;

            case State.nothing:
                if (flipsQueaue.Count > 0)
                {
                    if (flipsQueaue[0] == new Vector3(6, 6, 6))
                        Destroy(gameObject);
                    Flip(flipsQueaue[0]);
                    flipsQueaue.RemoveAt(0);
                }
                break;
        }


    }

    void FlipToPose(Vector3 targetPose)
    {
        transform.eulerAngles = Vector3.RotateTowards(transform.eulerAngles,
            new Vector3(targetRot.x, targetRot.y, targetRot.z),
            speedFlipping * Time.deltaTime, speedFlipping);

        float parabol = (90 - Mathf.Abs(transform.eulerAngles.y - 90)) / 90;
        float currentScale = 0.05f + parabol * scaleFlipping;
        float curretSideMove = parabol * sideMoveFlipping;

        transform.localScale = new Vector3(currentScale, currentScale, 0);
        transform.position = Vector3.MoveTowards(transform.position, targetPose, poseChangingSpeedFlipping * Time.deltaTime)
            + new Vector3(curretSideMove, curretSideMove, 0);
    }
}
