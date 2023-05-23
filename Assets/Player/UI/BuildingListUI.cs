using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingListUI : MonoBehaviour
{
    //Populates the building list with buttons that spawn Activate a building spawner and assign the building to it.
    //not doing research so all buildings are accessable from the start. 

    private ArmyMaster _playerArmy;
    private ArmyAssetList _armyAssets;
    [SerializeField]
    private GameObject _buildingUIButtonPrefab;
    [SerializeField]
    private Transform _ContentHolder;
    // Start is called before the first frame update
    void Start()
    {
        _playerArmy = GameMaster.Instance.GetPlayer();
        _armyAssets = _playerArmy != null ? GameMaster.Instance.GetArmyAssetList(_playerArmy.armyID) : null;
        PopulateUI();
    }

    //Populates the building buying UI.
    private void PopulateUI()
    {
        if (_armyAssets == null) { return; }
        foreach (BuildingScriptableObject building in _armyAssets.GetBuildings)
        {
            if(building.type == 200) { continue; }
            //add buttons to link building to the mouse.
            Instantiate(_buildingUIButtonPrefab, _ContentHolder).GetComponent<BuildingUIButton>().AssignBuildingToButton(building);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
