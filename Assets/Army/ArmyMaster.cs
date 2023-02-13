using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyMaster
{
    int _team;
    public int armyID;
    public bool IsPlayer;

    private List<BuildingBase> buildings = new();
    private List<UnitBase> units = new();


    public void AddBuildingToList(BuildingBase building)
    {
        buildings.Add(building);
    }
}
