using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : ArmyActorBase
{
    public BuildingScriptableObject scritableObjectOfThisBuilding;
    private List<UnitScritableObject> _unitConstructionQueue = new();
    private List<UnitShopUIButton> _buttons = new();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Check of the building CAN be build with the current resources on the army master and add it.
    public void AddtoMaster(ArmyMaster armyMaster)
    {
        if(armyMaster.Money < scritableObjectOfThisBuilding.cost)
        {
            Destroy(this.gameObject);
            return;
        }
        armyMaster.RemoveMoney(scritableObjectOfThisBuilding.cost);
        armyMaster.AddBuildingToList(this);
        _army = armyMaster;
    }


    public void SubToUIElement(UnitShopUIButton unitShopUIButton)
    {
        _buttons.Add(unitShopUIButton);
    }

    public void RemoveSubToUIElement(UnitShopUIButton unitShopUIButton)
    {
        _buttons.Remove(unitShopUIButton);
    }


    public int GetQueueCountOfUnit(UnitScritableObject unit)
    {
        int count = 0;
        foreach(UnitScritableObject unitScritable in _unitConstructionQueue)
        {
            if(unitScritable == unit) { count++; }
        }
        return count;
    }

    private void UpdateAllCounts()
    {
        foreach(UnitShopUIButton button in _buttons)
        {
            button.UpdateCount(GetQueueCountOfUnit(button.Unit));
        }
    }



    public void AddUnitToQueue(UnitScritableObject unitScritable)
    {
        _unitConstructionQueue.Add(unitScritable);
    }


    public void RemoveFromQueue(UnitScritableObject unitScritable)
    {
        _unitConstructionQueue.Remove(unitScritable);
    }



}
