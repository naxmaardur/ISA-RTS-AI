using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//A spawner for building for the player only
public class BuildingSpawner : Singleton<BuildingSpawner>
{
    private bool _active;
    private bool _overlapping;
    private BoxCollider _collider;
    private BuildingBase _buildingInstance;

    private void Awake()
    {
        Instance = this;
        _collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if(!_active) { return; }

        if(UtiltyFunctions.OverUI()) { return; }

       

        transform.position = UtiltyFunctions.getGridPointOnMap();

        //places building and deactivates
        if (Input.GetMouseButtonDown(0))
        {
            //check if not overlapping
            _buildingInstance.transform.position = transform.position;
            _buildingInstance.AddtoMaster(GameMaster.Instance.GetPlayer());
            _active = false;
            transform.position = new Vector3(0, 600, 0);
            _buildingInstance = null;
        }
        //removes the building and deactivates
        if (Input.GetMouseButtonDown(1))
        {
            _active = false;
            transform.position = new Vector3(0, 600, 0);
            if (_buildingInstance != null)
            {
                Destroy(_buildingInstance);
            }
        }
    }


    public void SetBuilding(BuildingScriptableObject building)
    {
        if(_buildingInstance != null)
        {
            Destroy(_buildingInstance);
        }
        _active = true;
        //set the preview and size of the trigger.
        _buildingInstance = Instantiate(building.buildingPrefab, new Vector3(0, 600, 0), Quaternion.identity).GetComponent<BuildingBase>();
        BoxCollider boxCollider = _buildingInstance.GetComponent<BoxCollider>();
        _collider.size = boxCollider.size;
        
    }


}
