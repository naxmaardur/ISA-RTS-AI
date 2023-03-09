using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArmyMaster
{
    int _team;
    public int armyID;
    public bool IsPlayer;

    private int _money;

    public int Money { get { return _money; } }
    [SerializeField]
    private List<BuildingBase> _buildings = new();
    private List<UnitBase> _units = new();

    public delegate void OnMoneyUpdatedEvent(int money);
    public OnMoneyUpdatedEvent OnMoneyUpdated;

    public delegate void OnActorDestroyedEvent(ArmyActorBase actor);
    public OnActorDestroyedEvent OnActorDestroyed;


    public List<BuildingBase> Buildings { get { return _buildings; } }

    public void AddBuildingToList(BuildingBase building)
    {
        _buildings.Add(building);
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
