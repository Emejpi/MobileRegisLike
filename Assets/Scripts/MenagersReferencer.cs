using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenagersReferencer : MonoBehaviour {

    public static CardsGenerator cardsGen; public static CardsGenerator GetCardsGen() { return cardsGen; }
    public static Deck deck; public static Deck GetDeck() { return deck; }
    public static Grave grave; public static Grave GetGrave() { return grave; }
    public static ButtonsGenerator buttonsMenager; public static ButtonsGenerator GetButtonsMenager() { return buttonsMenager; }
    public static PointsHoldersMenager pointsMenager; public static PointsHoldersMenager GetPointsMenager() { return pointsMenager; }

    void Start()
    {
        cardsGen = GameObject.Find("CardsGenerator").GetComponent<CardsGenerator>();
        deck = GameObject.Find("Deck").GetComponent<Deck>();
        grave = GameObject.Find("Grave").GetComponent<Grave>();
        buttonsMenager = GameObject.Find("BottomScreen").GetComponent<ButtonsGenerator>();
        pointsMenager = GameObject.Find("TopScreen").GetComponent<PointsHoldersMenager>();
    }

    public static void Reload()
    {
        grave.gameObject.AddComponent<ReloadOnTimer>();
    }

    public static void Exit()
    {
        Application.Quit();
    }
}
