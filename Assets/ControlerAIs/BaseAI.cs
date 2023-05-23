using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;

public class BaseAI : ControlerAI
{
    [SerializeField]
    AIBrainControler aIBrain;

    [SerializeField]
    BuildingSpot[] goldvains;
    [SerializeField]
    BuildingSpot[] buildingSpots;

    public void SetUpAIApponent(ArmyMaster army, ArmyAssetList armyAssets)
    {
        base.army = army;
        this.armyAssets = armyAssets;
        army.OnActorDestroyed += OnActorDestroyed;
        ConstructMine();
    }

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

    private void OnActorDestroyed(ArmyActorBase actor)
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
        foreach (BuildingBase building in army.Buildings)
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
        UnitScritableObject[] units = armyAssets.GetUnitsOfType(camp.scritableObjectOfThisBuilding.unitConstuctionType);
        camp.AddUnitToQueue(units[0]);
        army.RemoveMoney(units[0].cost);
    }


    public void ConstructCamp(int type)
    {
        BuildingScriptableObject camp = null;
        foreach(BuildingScriptableObject b in armyAssets.GetBuildingsOfType(0))
        {
            if(b.unitConstuctionType == type)
            {
                camp = b;
                break;
            }
        }
        if (camp == null) { return; }
        int location = Random.Range(0, buildingSpots.Length);
        if(buildingSpots[location].building != null) { return; }
        BuildingBase instance = SpawnBuilding(camp, buildingSpots[location].transform.position);
        buildingSpots[location].building = instance;
    }

    public void ConstructMine()
    {
        BuildingScriptableObject[] buildings = armyAssets.GetBuildingsOfType(1);
        List<BuildingBase> mines = new();

        foreach (BuildingBase building in army.Buildings)
        {
            if (building.scritableObjectOfThisBuilding.type == 1)
            {
                mines.Add(building);
            }
        }
        if(mines.Count == 8) { return; }

        foreach (BuildingSpot spot in goldvains)
        {
            if(spot.building == null)
            {
                spot.building = SpawnBuilding(buildings[0], spot.transform.position);
                break;
            }
        }
    }


    /*private void AddBuildingToCorrectList(BuildingBase building)
    {
        switch (building.scritableObjectOfThisBuilding.unitConstuctionType)
        {
            default: return;
            case 0:
                buildingsCType0.Add(building);
                break;
            case 1:
                buildingsCType1.Add(building);
                break;
            case 2:
                buildingsCType2.Add(building);
                break;
        }
    }*/


    BuildingBase SpawnBuilding(BuildingScriptableObject building, Vector3 position)
    {

        GameObject buildingObject = GameMaster.Instance.InstantiateObject(building.buildingPrefab, position);
        BuildingBase buildingInstance = buildingObject.GetComponent<BuildingBase>();
        buildingInstance.AddtoMaster(army);
        buildingInstance.PlacedEvent?.Invoke();
        buildingInstance.MaxHealth = building.health;
        buildingInstance.Health = building.health;
        //AddBuildingToCorrectList(buildingInstance);
        return buildingInstance;
    }

}
