using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphElement : MonoBehaviour {

    protected List<GraphElement> elements; //NIECH TO BEDZIE JEDYNE MIEJSCE SPRAWDZANIA ELEMENT TYPE. TO LISTA OBECNYCH BRANCHOW MODYFIKOWANA ZEWNETRZNIE
    // elements - konkretne signlowe dzieci // branches - moga zawierac paczki i elementy losowe
    protected List<GraphElement> branches;

    protected GraphElement trunk;

    public GraphElement portalBranch;
    public List<GraphElement> portalBranches;

    [System.Serializable]
    public struct UnlockableCond
    {
        public UnlockableHolder.UnlockableName name;
        public bool needed;
    }

    public UnlockableCond unlockableCond;
    public List<UnlockableCond> unlockableSCond;

    public bool playerPrefElem;
    public int playerPrefBasicValue;
    public string playerPrefName;

    public enum ElementType
    {
        single,
        pack,
        random,
        randomOnGameStart
    }
    public ElementType type;

    public int priority;

    public int daysAvaibleMin;
    public int daysAvaibleMax = 1000;

    public void SetPlayerPref(int value)
    {
        PlayerPrefs.SetInt(playerPrefName, value);

        if (!trunk)
            trunk = transform.parent.GetComponent<GraphElement>();

        if (value == 0)
            trunk.branches.Remove(this);
    }

    public bool IsUnlocked()
    {
        foreach (UnlockableCond unlocCond in unlockableSCond)
            if ((unlocCond.needed && !MenagersReferencer.GetUnlockablesManager().DoesItContain(unlocCond.name))
                || (!unlocCond.needed && MenagersReferencer.GetUnlockablesManager().DoesItContain(unlocCond.name))
                || (playerPrefElem && PlayerPrefs.GetInt(playerPrefName) == 0))
                return false;

        return true;
    }

    public int GetPriory()
    {
        if(!IsUnlocked())
                return -1;

        if (priority < -10)
            priority = -1;

        return priority;
    }

    public bool IsItAvaibleInDay(int day)
    {
        return day >= daysAvaibleMin && day <= daysAvaibleMax;
    }

    public int GetNumberOfBranches() { return branches.Count; }

    public GraphElement GetBranch(int index)
    {
        return branches[index];
    }

    public int GetNumberOfElements() { return elements.Count; }

    public GraphElement GetElement(int index)
    {
        return elements[index];
    }

    void Start()
    {
        Prepare();
    }

    int GetHighestBranchPririty()
    {
        int highestProry = 0;

        foreach(GraphElement branch in branches)
        {
            if (highestProry < branch.GetPriory())
                highestProry = branch.GetPriory();
        }

        return highestProry;
    }

    List<GraphElement> GetBranchesWithHighestPriority()
    {
        int highestProry = GetHighestBranchPririty();

        List<GraphElement> branchesSignificent = new List<GraphElement>();

        foreach(GraphElement branch in branches)
        {
            if ((branch.GetPriory() == highestProry || branch.GetPriory() == 0))
                branchesSignificent.Add(branch);
        }

        return branchesSignificent;
    }

    public void UpdateElements()
    {
        elements = new List<GraphElement>();

        List<GraphElement> branchesSignificant = GetBranchesWithHighestPriority();

        foreach(GraphElement branch in branchesSignificant)
        {
            elements.AddRange(branch.GetElements());
        }
    }

    List<GraphElement> GetElements()
    {
        elements = new List<GraphElement>();

        List<GraphElement> branchesSignificant = GetBranchesWithHighestPriority();

        switch (type)
        {
            case ElementType.single:
                elements.Add(this);
                break;

            case ElementType.random:
                elements.AddRange(branchesSignificant[Random.Range(0, branchesSignificant.Count)].GetElements());
                break;

            case ElementType.pack:
                foreach (GraphElement branchOfPack in branchesSignificant)
                {
                    elements.AddRange(branchOfPack.GetElements());
                }
                break;

            case ElementType.randomOnGameStart:
                foreach (GraphElement element in elements)
                {
                    elements.AddRange(element.GetElements());
                }
                break;
        }

        return elements;
    }

    public void AddPriory(int value)
    {
        priority += value;
        if (priority <= -10)
            priority = -1;
    }

    // Use this for initialization
    protected void Prepare()
    {
        branches = new List<GraphElement>();

        for (int i = 0; i < transform.childCount; i++)
        {
            branches.Add(transform.GetChild(i).GetComponent<GraphElement>());
            //transform.GetChild(i).GetComponent<GraphElement>().trunk = this;
        }

        if(portalBranch)
            branches.Add(portalBranch);
        if(portalBranches.Count > 0)
        foreach (GraphElement branch in portalBranches)
        {
            branches.Add(branch);
        }

        foreach (GraphElement branch in branches)
            branch.trunk = this;

        if (type == ElementType.randomOnGameStart)
        {
            while(branches.Count > 1)
            {
                DetachRandomBranch();
            }
            type = ElementType.pack;
        }

        if(unlockableCond.name != UnlockableHolder.UnlockableName.none)
        {
            unlockableSCond.Add(unlockableCond);
        }

        foreach (GraphElement branch in branches)
            if (branch.playerPrefElem)
            {
                if (!PlayerPrefs.HasKey(branch.playerPrefName))
                    branch.SetPlayerPref(branch.playerPrefBasicValue);

                if (PlayerPrefs.GetInt(branch.playerPrefName) == 0)
                {
                    branch.SetPlayerPref(0);
                }

            }


    }

    void DetachRandomBranch()
    {
        DetachBranch(Random.Range(0, branches.Count));
    }

    void DetachBranch(int index)
    {
        branches.RemoveAt(index);
    }
}
