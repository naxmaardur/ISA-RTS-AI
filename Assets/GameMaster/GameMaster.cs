using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : Singleton<GameMaster>
{
    [SerializeField]
    private LayerMask _groundlayerMask;
    public LayerMask GroundLayer { get { return _groundlayerMask; } }

    private ArmyAssetList[] _armyAssets = {null,null };
    [SerializeField]
    private ArmyMaster[] _armyMasters = {null,null};
    private TacticalAIManager[] _enemyAi = { null };
    private Grid grid;



    public ArmyAssetList GetArmyAssetList(int index)
    {
        return _armyAssets[index];
    }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        new Grid(200, 200, 1);
        _armyAssets[0] = new ArmyAssetList(0);
        //temp code{
        _armyMasters[0] = new ArmyMaster();
        _armyMasters[0].IsPlayer = true;
        _armyMasters[1] = new ArmyMaster();
        _armyMasters[1].IsPlayer = false;
        _enemyAi[0] = new TacticalAIManager();
        _enemyAi[0].SetUpAIApponent(_armyMasters[1], _armyAssets[0]);
        //}
        grid = Grid.Instance;
        grid.SetupCorotines(this);
        SpawnStartingBuildings();
    }


    void SpawnStartingBuildings()
    {
        BasePoint[] points = FindObjectsOfType<BasePoint>();
        GameObject building = Instantiate(_armyAssets[0].GetBuildingsOfType(200)[0].buildingPrefab, grid.getSafeNodeFromWordlPoint(points[0].transform.position)._worldPoint, Quaternion.identity);
        building.GetComponent<BuildingBase>().AddtoMaster(_armyMasters[1]);
        building = Instantiate(_armyAssets[0].GetBuildingsOfType(200)[0].buildingPrefab, grid.getSafeNodeFromWordlPoint(points[1].transform.position)._worldPoint, Quaternion.identity);
        building.GetComponent<BuildingBase>().AddtoMaster(_armyMasters[0]);
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
        foreach(TacticalAIManager tacticalAI in _enemyAi)
        {
            tacticalAI.Update();
        }
        
    }






    //Unity functions that can only be called in monobehaviors that will be called in non monobehaviors
    //So that the AI can Spawn objects without the need of beign a monobehavior
    public GameObject InstantiateObject(GameObject gameObject,Vector3 position)
    {
        return Instantiate(gameObject, position, Quaternion.identity);
    }

    //So that the AI can have Coroutines without the need of beign a monobehavior
    //I have no idea if this will work properly
    public Coroutine StartRemoteCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }

    public void StopRemoteCoroutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }


    
}
