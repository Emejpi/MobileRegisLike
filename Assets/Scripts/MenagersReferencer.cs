using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenagersReferencer : MonoBehaviour
{

    static CardsGenerator cardsGen; public static CardsGenerator GetCardsGen() { return cardsGen; }
    static Deck deck; public static Deck GetDeck() { return deck; }
    static Grave grave; public static Grave GetGrave() { return grave; }
    static ButtonsGenerator buttonsMenager; public static ButtonsGenerator GetButtonsMenager() { return buttonsMenager; }
    static PointsHoldersMenager pointsMenager; public static PointsHoldersMenager GetPointsMenager() { return pointsMenager; }
    static UnlockableHolder unlockablesManager; public static UnlockableHolder GetUnlockablesManager() { return unlockablesManager; }

    void Start()
    {
        cardsGen = GameObject.Find("CardsGenerator").GetComponent<CardsGenerator>();
        deck = GameObject.Find("Deck").GetComponent<Deck>();
        grave = GameObject.Find("Grave").GetComponent<Grave>();
        buttonsMenager = GameObject.Find("BottomScreen").GetComponent<ButtonsGenerator>();
        pointsMenager = GameObject.Find("TopScreen").GetComponent<PointsHoldersMenager>();
        unlockablesManager = GameObject.Find("Unlockables").GetComponent<UnlockableHolder>();
    }

    public static void GameOver()
    {
        GetDeck().DestroyAll();
        GetGrave().DestroyAll();
        Reload();
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
