using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenagingObject : MonoBehaviour {

    MenagersReferencer menagersReferencer;

    protected MenagersReferencer GetMenagersReferencer()
    {
        if(!menagersReferencer)
        {
            menagersReferencer = GameObject.Find("Canvas").GetComponent<MenagersReferencer>();
        }
        return menagersReferencer;
    }
}
