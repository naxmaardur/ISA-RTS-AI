using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyAssetList
{
    private BuildingScriptableObject[] buildings;
    private UnitScritableObject[] units;
    public BuildingScriptableObject[] GetBuildings { get { return buildings; } }
    public UnitScritableObject[] GetUnits { get { return units; } }

    public ArmyAssetList(int armyID)
    {
        GetBuildingsFromResources(armyID);
        GetUnitsFromResources(armyID);
    }

    //getting all building ScritableObjects from the resource folder 
    private void GetBuildingsFromResources(int armyID)
    {
        List<BuildingScriptableObject> buildings = new();

        Object[] objects = Resources.LoadAll("Army"+ armyID +"/Buildings", typeof(BuildingScriptableObject));
        foreach (Object o in objects)
        {
            BuildingScriptableObject b = (BuildingScriptableObject)o;
            buildings.Add(b);
        }
        this.buildings = buildings.ToArray();
    }
    //getting all Unit ScritableObjects from the resource folder 
    private void GetUnitsFromResources(int armyID)
    {
        List<UnitScritableObject> units = new();
        Object[] objects = Resources.LoadAll("Army"+ armyID +"Units", typeof(UnitScritableObject));
        foreach (Object o in objects)
        {
            UnitScritableObject b = (UnitScritableObject)o;
            units.Add(b);
        }
        this.units = units.ToArray();
    }


    
    
}
