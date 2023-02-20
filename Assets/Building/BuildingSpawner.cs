using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//A spawner for building for the player only
public class BuildingSpawner : Singleton<BuildingSpawner>
{
    private bool _active;
    private BoxCollider _collider;
    private BuildingBase _buildingInstance;
    private float _maxDistance = 400;
    private ArmyMaster _armyMaster;
    private Vector3 _lastPos;
    [SerializeField]
    private bool _lastDistanceResult;
    [SerializeField]
    private bool _lastOverlapResult;
    [SerializeField]
    private Transform _sizeRenderer;
    [SerializeField]
    private Material _canPlaceMaterial;
    [SerializeField]
    private Material _canNotPlaceMaterial;
    [SerializeField]
    private MeshRenderer _meshRenderer;


    private void Awake()
    {
        Instance = this;
        _collider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        _armyMaster = GameMaster.Instance.GetPlayer();
    }

    private void OnDisable()
    {
        Instance = null;
    }

    private void Update()
    {
        if(_armyMaster == null) { return; }
        if(!_active) { return; }

        //removes the building and deactivates
        if (Input.GetMouseButtonDown(1))
        {
            _active = false;
            transform.position = new Vector3(0, 600, 0);
            if (_buildingInstance != null)
            {
                Destroy(_buildingInstance.gameObject);
            }
        }

        if (UtiltyFunctions.OverUI()) { _meshRenderer.material = _canNotPlaceMaterial; return; }
        if (!UtiltyFunctions.OverMap()) { _meshRenderer.material = _canNotPlaceMaterial; return; }
        transform.position = UtiltyFunctions.getGridPointOnMap();
        if(transform.position != _lastPos)
        {
            _lastPos = transform.position;
            _lastDistanceResult = CheckDistance();
            _lastOverlapResult = CheckOverlap();
        }
        if(!_lastDistanceResult || _lastOverlapResult)
        {
            _meshRenderer.material = _canNotPlaceMaterial;
            return;
        }
        else
        {
            _meshRenderer.material = _canPlaceMaterial;
        }

        //places building and deactivates
        if (Input.GetMouseButtonDown(0))
        {
            //check if not overlapping
            _buildingInstance.transform.position = transform.position;
            _buildingInstance.AddtoMaster(_armyMaster);
            _active = false;
            transform.position = new Vector3(0, 600, 0);
            _buildingInstance = null;
        }
    }

    //Get the distance to the closest building of the player army and check it against the max distance
    private bool CheckDistance()
    {
        float distance = Mathf.Infinity;
        if(_armyMaster.Buildings.Count == 0) { _lastDistanceResult = true; return true; }//temp code since there is not initial starting building right now
        foreach(BuildingBase building in _armyMaster.Buildings)
        {
            float calcDistance = Vector3.Distance(transform.position, building.transform.position);
            if (calcDistance < distance)
            {
                distance = calcDistance;
            }
        }
        if(distance < _maxDistance){ return true;}
        return false;
    }

    private bool CheckOverlap()
    {
        Collider[] colliders = Physics.OverlapBox(_collider.center+transform.position, _collider.size);
        foreach(Collider col in colliders)
        {
            Debug.Log(col.name);
        }
        if(colliders.Length != 0){ return true;}
        return false;
    }

    public void SetBuilding(BuildingScriptableObject building)
    {
        if(_buildingInstance != null)
        {
            Destroy(_buildingInstance.gameObject);
        }
        _active = true;
        //set the preview and size of the trigger.
        _buildingInstance = Instantiate(building.buildingPrefab, new Vector3(0, 600, 0), Quaternion.identity).GetComponent<BuildingBase>();
        BoxCollider boxCollider = _buildingInstance.GetComponent<BoxCollider>();
        _collider.size = boxCollider.size / 2.1f;
        _collider.center = boxCollider.center;
        _sizeRenderer.localPosition = boxCollider.center;
        _sizeRenderer.localScale = boxCollider.size;
    }

    public bool GetIsActive()
    {
        return _active;
    }
}
