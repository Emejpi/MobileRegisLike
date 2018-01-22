using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsHolder : MonoBehaviour {

    List<Option> options;

    public int GetNumberOfOptions() { return options.Count; }

    public OptionsHolder(Option option)
    {
        options = new List<Option>();
        options.Add(option);
    }

    public Option GetOption(int index)
    {
        //if (index >= options.Count)
        //    return new Option(Color.white,"...");
        return options[index];
    }

	// Use this for initialization
	void Start () {
        options = new List<Option>();

        for(int i = 0; i < transform.childCount; i++)
        {
            options.Add(transform.GetChild(i).GetComponent<Option>());
        }
	}

}
