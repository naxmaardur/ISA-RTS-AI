using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : Singleton<GameMaster>
{
    [SerializeField]
    private LayerMask _groundlayerMask;
    public LayerMask GroundLayer { get { return _groundlayerMask; } }

    private ArmyAssetList[] armyAssets = {null,null };
    [SerializeField]
    private ArmyMaster[] _armyMasters = {null,null};



    public ArmyAssetList GetArmyAssetList(int index)
    {
        return armyAssets[index];
    }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        armyAssets[0] = new ArmyAssetList(0);
        //temp code{
        _armyMasters[0] = new ArmyMaster();
        _armyMasters[0].IsPlayer = true;
        //}
    }

    //find the players army in the list of all armies
    public ArmyMaster GetPlayer()
    {
        foreach (ArmyMaster armyMaster in _armyMasters )
        {
            if(armyMaster.IsPlayer) { return armyMaster; }
        }
        return null;
    }

    public ArmyMaster GetArmyByIndex(int index)
    {
        return _armyMasters[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
