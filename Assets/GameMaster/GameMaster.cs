using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : Singleton<GameMaster>
{
    private LayerMask _groundlayerMask;
    public LayerMask GroundLayer { get { return _groundlayerMask; } }

    private ArmyAssetList[] armyAssets = {null,null };
    private ArmyMaster[] ArmyMasters = {null,null};



    public ArmyAssetList GetArmyAssetList(int index)
    {
        return armyAssets[index];
    }

    // Start is called before the first frame update
    void Awake()
    {
        armyAssets[0] = new ArmyAssetList(0);
    }

    //find the players army in the list of all armies
    public ArmyMaster GetPlayer()
    {
        foreach (ArmyMaster armyMaster in ArmyMasters )
        {
            if(armyMaster.IsPlayer) { return armyMaster; }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
