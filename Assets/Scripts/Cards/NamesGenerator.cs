using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamesGenerator : MonoBehaviour {

    List<NamesHolder> namesHolders;

	// Use this for initialization
	void Start () {
        namesHolders = new List<NamesHolder>();

		for(int i = 0; i < transform.childCount; i++)
        {
            namesHolders.Add(transform.GetChild(i).GetComponent<NamesHolder>());
        }
	}
	
    public string GetNameOfType(Card.Type type)
    {
        foreach (NamesHolder namesHolder in namesHolders)
            if (namesHolder.type == type)
                return namesHolder.nameOfType;

        return "";
    }

	public string GetRandomName(Card.Type type)
    {
        foreach (NamesHolder namesHolder in namesHolders)
            if (namesHolder.type == type)
                return namesHolder.GetRandomName();

        return "error";
    }
}
