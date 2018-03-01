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

    public PointsHolder GetPointsHolder(PointsHolder.PointsType type)
    {
        foreach (PointsHolder pointsHolder in pointsHolders)
            if (pointsHolder.type == type)
                return pointsHolder;
        return pointsHolders[0];
    }

    public void AddPoints(int value, PointsHolder.PointsType type)
    {
        GetPointsHolder(type).Add(value);
    }

    //public void AddPoints(int value, Card.Type type, string name)
    //{
    //    GetPointsHolder(type).Add(value, name);
    //}

    public int GetValue(PointsHolder.PointsType type)
    {
        return GetPointsHolder(type).value;
    }

    public void CheckForHL()
    {
        foreach (PointsHolder pointsH in pointsHolders)
            pointsH.CheckForTooHighTooLow();
    }

    // Use this for initialization
    void Start () {
        UpdatePointsHolders();
	}
}
