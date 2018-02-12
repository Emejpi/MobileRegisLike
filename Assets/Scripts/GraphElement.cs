using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphElement : MonoBehaviour {

    public List<GraphElement> publicBranches; //NIECH TO BEDZIE JEDYNE MIEJSCE SPRAWDZANIA ELEMENT TYPE. TO LISTA OBECNYCH BRANCHOW MODYFIKOWANA ZEWNETRZNIE

    protected List<GraphElement> branches;

    protected GraphElement trunk;

    public GraphElement portalBranch;
    public List<GraphElement> portalBranches;

    public int GetNumberOfBranches() { return branches.Count; }

    public enum ElementType
    {
        single,
        pack,
        random
    }
    public ElementType type;

    public int pritity;

    public GraphElement GetBranch(int index)
    {
        return branches[index];
    }

    void Start()
    {
        Prepare();
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
    }
}
