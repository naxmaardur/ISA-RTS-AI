using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int _currentArmyIndex = 0;

    private List<UnitBase> units = new();

    public int CurrentArmyIndex {get { return _currentArmyIndex; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnMouseUpdates();


    }


    void OnMouseUpdates()
    {
        if (BuildingSpawner.Instance.GetIsActive()) { return; }
        if (UtiltyFunctions.OverUI()) { return; }
        OnMouseDownLeft();
        OnMouseDownRight();
    }

    private void OnMouseDownRight()
    {
        
    }

    private void OnMouseDownLeft()
    {
        if (Input.GetMouseButtonDown(0))
        {
            units.Clear();
            GameObject gameObject = UtiltyFunctions.GetObjectAtMousePoint();
            ArmyActorBase armyActor = gameObject.GetComponent<ArmyActorBase>();
            if(armyActor == null) { return; }
            BuildingSelection(armyActor);
            UnitSelection(armyActor);
        }
    }

    private void BuildingSelection(ArmyActorBase armyActor)
    {
        BuildingBase building;
        try
        {
            building = (BuildingBase)armyActor;
        }
        catch
        {
            Debug.Log("not a building");
            return;
        }
        if(building.ArmyMaster != GameMaster.Instance.GetPlayer()) { return; }
        //building UI pop up until you click on the back button or click anywhere on the screen that is not UI. the check for that should be in the UI code.

    }

    private void UnitSelection(ArmyActorBase armyActor)
    {
        UnitBase unit;
        try
        {
            unit = (UnitBase)armyActor;
        }
        catch
        {
            return;
        }
        if (unit.ArmyMaster != GameMaster.Instance.GetPlayer()) { return; }
        //Add unit to the list of unites that should be controled until the player left clicks again.
    }
}
