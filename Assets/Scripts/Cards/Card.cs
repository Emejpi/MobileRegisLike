using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : CardStatisctics {
    public int index;

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

    void Start()
    {
            
            //addFlippingToQueue = false;

            //currentRightRot = 0;
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
        extraSpeed = 0;

        if(currentRightRot == 180)
            deck.GenerateButtons(this);
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
                10 + (currentRightRot == 180 ? 0 : deck.RandomMP(deck.rotationRange)));
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
                        (speedFlipping + extraSpeed)* Time.deltaTime, speedFlipping + extraSpeed);

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
