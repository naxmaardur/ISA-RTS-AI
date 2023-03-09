using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalAIManager
{
    private ArmyMaster _army;
    private ArmyAssetList _armyAssets;

    private List<BuildingBase> buildingsCType0 = new();
    private List<BuildingBase> buildingsCType1 = new();
    private List<BuildingBase> buildingsCType2 = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }


    public void SetUpAIApponent(ArmyMaster army, ArmyAssetList armyAssets)
    {
        _army = army;
        _armyAssets = armyAssets;
    }

    private void AddBuildingToCorrectList(BuildingBase building)
    {
        switch (building.scritableObjectOfThisBuilding.unitConstuctionType)
        {
            default: return;
            case 0: buildingsCType0.Add(building);
                break;
            case 1: buildingsCType1.Add(building);
                break;
            case 2: buildingsCType2.Add(building);
                break;
        }
    }


    void SpawnBuilding(BuildingScriptableObject building)
    {
        Vector3 position = GetBestLocationForBuilding(building);
        GameObject buildingObject = GameMaster.Instance.InstantiateObject(building.buildingPrefab, position);
        BuildingBase buildingInstance = buildingObject.GetComponent<BuildingBase>();
        buildingInstance.AddtoMaster(_army);
        buildingInstance.PlacedEvent?.Invoke();
        _army.RemoveMoney(buildingInstance.scritableObjectOfThisBuilding.cost);
        AddBuildingToCorrectList(buildingInstance);
    }


    private Vector3 GetBestLocationForBuilding(BuildingScriptableObject building)
    {
        //TODO ADD code to decide where the building should be placed using Influence maps and utility scores

        return Vector3.zero;
    }

    void BuildUnit(UnitScritableObject unit)
    {
        List<BuildingBase> list;
        switch (unit.type)
        {
            default: list = buildingsCType0;
                break;
            case 0:
                list = buildingsCType0;
                break;
            case 1:
                list = buildingsCType1;
                break;
            case 2:
                list = buildingsCType2;
                break;
        }

        int UnitsInconstruction = 90000;
        BuildingBase building = null;
        foreach(BuildingBase buildingBase in list)
        {
            if(buildingBase.UnitQueueLength > UnitsInconstruction) { continue; }
            UnitsInconstruction = buildingBase.UnitQueueLength;
            building = buildingBase;
        }
        if(building == null) { return; }
        _army.RemoveMoney(unit.cost);
        building.AddUnitToQueue(unit);
    }



}
