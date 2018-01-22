using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildsReferencer : MenagingObject {

    public GameObject GetChild(int num)
    {
        return transform.GetChild(num).gameObject;
    }
}
