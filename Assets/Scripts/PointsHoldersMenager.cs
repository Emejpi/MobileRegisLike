using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsHoldersMenager : MonoBehaviour {

    public List<PointsHolder> pointsHolders;

    void UpdatePointsHolders()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            pointsHolders.Add(transform.GetChild(i).GetComponent<PointsHolder>());
        }
    }

    public PointsHolder GetPointsHolder(PointsHolder.Type type)
    {
        foreach (PointsHolder pointsHolder in pointsHolders)
            if (pointsHolder.type == type)
                return pointsHolder;
        return pointsHolders[0];
    }

    public void AddPoints(int value, PointsHolder.Type type)
    {
        GetPointsHolder(type).Add(value);
    }

    public int GetValue(PointsHolder.Type type)
    {
        return GetPointsHolder(type).value;
    }

    // Use this for initialization
    void Start () {
        UpdatePointsHolders();
	}
}
