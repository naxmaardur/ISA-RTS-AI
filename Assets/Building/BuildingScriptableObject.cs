using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "ArmyObjects/building", order = 1)]
//Contains the information of a building before it is build. 
public class BuildingScriptableObject : ScriptableObject
{

    public GameObject buildingPrefab;

}
