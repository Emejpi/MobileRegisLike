using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStatisctics : BasicActions {
    [Header("Stats")]
    public OptionsHolder optionsHolder;

    public enum Type
    {
        regular,
        room,
        person,
        monster
    }
    public Type type;

    public string tittle;

    public bool startDeckCard;
}
