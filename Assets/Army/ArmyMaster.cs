using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyMaster
{
    int _team;
    public int armyID;
    public bool IsPlayer;

    private int _money;

    public int Money { get { return _money; } }

    private List<BuildingBase> buildings = new();
    private List<UnitBase> units = new();

    public delegate void OnMoneyUpdatedEvent(int money);
    public OnMoneyUpdatedEvent OnMoneyUpdated;



    public void AddBuildingToList(BuildingBase building)
    {
        buildings.Add(building);
    }

    public void RemoveMoney(int toRemove)
    {
        _money -= toRemove;
        Mathf.Clamp(_money, 0, int.MaxValue);
        OnMoneyUpdated?.Invoke(_money);
    }

    public void AddMoney(int toAdd)
    {
        _money += toAdd;
        Mathf.Clamp(_money, 0, int.MaxValue);
        OnMoneyUpdated?.Invoke(_money);
    }
}
