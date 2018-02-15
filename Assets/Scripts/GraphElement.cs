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

    public enum ElementType
    {
        single,
        pack,
        random,
        randomOnGameStart
    }
    public ElementType type;

    public int priority;

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
            if (highestProry < branch.priority)
                highestProry = branch.priority;
        }

        return highestProry;
    }

    List<GraphElement> GetBranchesWithHighestPriority()
    {
        int highestProry = GetHighestBranchPririty();

        List<GraphElement> branchesSignificent = new List<GraphElement>();

        foreach(GraphElement branch in branches)
        {
            if (branch.priority == highestProry)
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
