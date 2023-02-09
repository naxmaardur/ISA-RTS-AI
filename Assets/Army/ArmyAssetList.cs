using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyAssetList : MonoBehaviour
{
    GameObject[] buildings;
    GameObject[] units;


    private void GetBuildings()
    {
        Object[] objects = Resources.LoadAll("Buildings", typeof(BuildingObject));
        foreach (Object o in objects)
        {
            BuildingObject b = (BuildingObject)o;
            buildings.Add(b);
        }
    }
    //getting all unitObject from the resource folder 
    private void GetUnits()
    {
        Object[] objects = Resources.LoadAll("Units", typeof(UnitObject));
        foreach (Object o in objects)
        {
            UnitObject b = (UnitObject)o;
            units.Add(b);
        }
    }

}
