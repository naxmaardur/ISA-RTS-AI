using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : ArmyActorBase
{
    public BuildingScriptableObject scritableObjectOfThisBuilding;
    private List<UnitScritableObject> _unitConstructionQueue = new();
    private List<UnitShopUIButton> _buttons = new();
    private Coroutine _constructionCoroutine;
    private bool _constructionCoroutineIsRunning;
    [SerializeField]
    private Transform SpawnPoint;
    public delegate void Placed();
    public Placed PlacedEvent;
    public int UnitQueueLength { get { return _unitConstructionQueue.Count; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_unitConstructionQueue.Count == 0) { return; }
        if (_constructionCoroutine == null || !_constructionCoroutineIsRunning)
        {
            _constructionCoroutine = StartCoroutine(ConstructUnit(_unitConstructionQueue[0].constructTime));
        }


    }

    //Check of the building CAN be build with the current resources on the army master and add it.
    public void AddtoMaster(ArmyMaster armyMaster)
    {
        if(armyMaster.Money < scritableObjectOfThisBuilding.cost)
        {
            Destroy(this.gameObject);
            return;
        }
        armyMaster.RemoveMoney(scritableObjectOfThisBuilding.cost);
        armyMaster.AddBuildingToList(this);
        _army = armyMaster;
        Grid.Instance.AddEntityToALLMaps(this);
        SetNewGridPosition();
    }


    private void OnDisable()
    {
        Grid.Instance.RemoveEntityFromALLMaps(this);
    }

    public void SubToUIElement(UnitShopUIButton unitShopUIButton)
    {
        _buttons.Add(unitShopUIButton);
    }

    public void RemoveSubToUIElement(UnitShopUIButton unitShopUIButton)
    {
        _buttons.Remove(unitShopUIButton);
    }


    public int GetQueueCountOfUnit(UnitScritableObject unit)
    {
        int count = 0;
        foreach(UnitScritableObject unitScritable in _unitConstructionQueue)
        {
            if(unitScritable == unit) { count++; }
        }
        return count;
    }

    private void UpdateAllCounts()
    {
        foreach(UnitShopUIButton button in _buttons)
        {
            button.UpdateCount(GetQueueCountOfUnit(button.Unit));
        }
    }



    public void AddUnitToQueue(UnitScritableObject unitScritable)
    {
        _unitConstructionQueue.Add(unitScritable);
    }


    public void RemoveFromQueue(UnitScritableObject unitScritable)
    {
        UnitScritableObject old = _unitConstructionQueue[0];

        _unitConstructionQueue.Remove(unitScritable);

        if(old != _unitConstructionQueue[0])
        {
            _constructionCoroutineIsRunning = false;
            StopCoroutine(_constructionCoroutine);
        }
    }

    public IEnumerator ConstructUnit(int TimeInSeconds)
    {
        _constructionCoroutineIsRunning = true;
        yield return new WaitForSeconds(TimeInSeconds);

        //spawn unit;
        GameObject gameObject = Instantiate(_unitConstructionQueue[0].unitPrefab, SpawnPoint.position, Quaternion.identity);
        UnitBase unit = gameObject.GetComponent<UnitBase>();
        unit.SetArmy(ArmyMaster);
        unit.unitScritable = _unitConstructionQueue[0];
        unit.Health = _unitConstructionQueue[0].health;

        _unitConstructionQueue.RemoveAt(0);
        UpdateAllCounts();
        _constructionCoroutineIsRunning = false;
        StopCoroutine(_constructionCoroutine);
    }

}
