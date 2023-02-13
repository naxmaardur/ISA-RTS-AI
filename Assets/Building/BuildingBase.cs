using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    private int _team;
    private ArmyMaster _army;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddtoMaster(ArmyMaster armyMaster)
    {
        armyMaster.AddBuildingToList(this);
        _army = armyMaster;
    }
}
