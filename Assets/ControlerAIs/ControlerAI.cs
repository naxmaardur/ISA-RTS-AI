using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerAI : MonoBehaviour
{
    public ArmyMaster army;
    public ArmyAssetList armyAssets;

    public int GetMineCount()
    {
        int i = 0;

        foreach (BuildingBase building in army.Buildings)
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

        foreach (BuildingBase building in army.Buildings)
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

        foreach (BuildingBase building in army.Buildings)
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
        return army.Units.Count;
    }
    public int GetUnitCountByType(int type)
    {
        int i = 0;

        foreach (UnitBase unit in army.Units)
        {
            if (unit.unitScritable.type == type)
            {
                i++;
            }
        }
        return i;
    }

    public int GetShortestQueueByType(int type)
    {
        int i = 20000;

        foreach (BuildingBase building in army.Buildings)
        {
            if (building.scritableObjectOfThisBuilding.type == 0 && building.scritableObjectOfThisBuilding.unitConstuctionType == type)
            {
                if (building.UnitQueueLength < i)
                {
                    i = building.UnitQueueLength;
                }
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

    public int GetMoney()
    {
        Debug.Log(army.Money);
        return army.Money;
    }
}
