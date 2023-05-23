using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerAI : MonoBehaviour
{
    protected ArmyMaster Army;
    public int GetMineCount()
    {
        int i = 0;

        foreach (BuildingBase building in Army.Buildings)
        {
            if (building.scritableObjectOfThisBuilding.type == 1)
            {
                i++;
            }
        }
        return i;
    }
    public int GetCampCount(int unitType)
    {
        int i = 0;

        foreach (BuildingBase building in Army.Buildings)
        {
            if (building.scritableObjectOfThisBuilding.type == 0 && building.scritableObjectOfThisBuilding.unitConstuctionType == unitType)
            {
                i++;
            }
        }
        return i;
    }
    public int GetUnitsInconstruction(int unitType)
    {
        int i = 0;

        foreach (BuildingBase building in Army.Buildings)
        {
            if (building.scritableObjectOfThisBuilding.type == 0 && building.scritableObjectOfThisBuilding.unitConstuctionType == unitType)
            {
                i += building.UnitQueueLength;
            }
        }
        return i;
    }
    public int GetEnemyUnitsInconstruction(int unitType)
    {
        int i = 0;

        foreach (BuildingBase building in GameMaster.Instance.GetPlayer().Buildings)
        {
            if (building.scritableObjectOfThisBuilding.type == 0 && building.scritableObjectOfThisBuilding.unitConstuctionType == unitType)
            {
                i += building.UnitQueueLength;
            }
        }
        return i;
    }
    public int GetUnitCount()
    {
        return Army.Units.Count;
    }
    public int GetUnitCountByType(int type)
    {
        int i = 0;

        foreach (UnitBase unit in Army.Units)
        {
            if (unit.unitScritable.type == type)
            {
                i++;
            }
        }
        return i;
    }


    public int GetEnemyUnitCount()
    {
        return GameMaster.Instance.GetPlayer().Units.Count;
    }
    public int GetEnemyUnitCountByType(int type)
    {
        int i = 0;

        foreach (UnitBase unit in GameMaster.Instance.GetPlayer().Units)
        {
            if (unit.unitScritable.type == type)
            {
                i++;
            }
        }
        return i;
    }
}
