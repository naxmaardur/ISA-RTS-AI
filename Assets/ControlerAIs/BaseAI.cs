using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;

public class BaseAI : ControlerAI
{
    [SerializeField]
    AIBrainControler aIBrain;
    // Start is called before the first frame update
    void Start()
    {
        aIBrain.SetNPC(this);
        StartCoroutine(delayedUpdates());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator delayedUpdates()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            aIBrain.DecideBestAction();
        }
    }


    public void ConstructUnit(int type)
    {
        BuildingBase camp = null;
        int queueLength = 20000;
        foreach (BuildingBase building in Army.Buildings)
        {
            if (building.scritableObjectOfThisBuilding.type == 0 && building.scritableObjectOfThisBuilding.unitConstuctionType == type)
            {
                if(building.UnitQueueLength < queueLength)
                {
                    camp = building;
                    queueLength = building.UnitQueueLength;
                }
            }
        }

        if(camp == null) { return; }
        UnitScritableObject[] units = GameMaster.Instance.GetArmyAssetList(camp.ArmyMaster.armyID).GetUnitsOfType(camp.scritableObjectOfThisBuilding.unitConstuctionType);
        camp.AddUnitToQueue(units[0]);
    }


    public void ConstructCamp()
    {

    }

    public void ConstructMine()
    {

    }

}
