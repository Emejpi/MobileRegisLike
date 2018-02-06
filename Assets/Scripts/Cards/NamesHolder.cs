using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamesHolder : MonoBehaviour {

    public Card.Type type;

    public string nameOfType;

    public List<string> names;

    public void AddName(string name)
    {
        names.Add(name);
    }

    public string TestRandomName()
    {
        if (names.Count == 0)
            return "YOU";

        int index = 0;//Random.Range(0, names.Count);
        string name = names[index];
        return name;
    }

    public string GetRandomName()
    {
        string name = TestRandomName();
        names.Remove(name);
        return name;
    }
}
