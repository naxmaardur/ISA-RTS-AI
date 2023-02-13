using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUIButton : MonoBehaviour
{
    public BuildingScriptableObject building;

    public void AssignBuildingToSpawner()
    {
        BuildingSpawner.Instance.SetBuilding(building);
    }
}
