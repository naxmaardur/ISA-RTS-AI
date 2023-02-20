using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "ArmyObjects/Unit", order = 1)]
//Contains the information of a Unit before it is made. 
public class UnitScritableObject : ScriptableObject
{
    public int type;
    public int cost;
    public int constructTime;
    public GameObject unitPrefab;

    public int health;
    public int speed;
    public int range;
    public int attack;
}
