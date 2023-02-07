using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingListUI : MonoBehaviour
{
    //Populates the building list with buttons that spawn Activate a building spawner and assign the building to it.
    //not doing research so all buildings are accessable from the start. 
    private ArmyAssetList _armyAssets = GameMaster.Instance.GetArmyAssetList(0);  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
