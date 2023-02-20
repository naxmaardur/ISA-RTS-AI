using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : ArmyActorBase
{
    public BuildingScriptableObject scritableObjectOfThisBuilding;


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
}
