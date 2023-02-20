using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShopUIlist : Singleton<UnitShopUIlist>
{
    [SerializeField]
    private GameObject _unitButtonPrefab;

    [SerializeField]
    private Transform _content;

    public BuildingBase building;
    private List<GameObject> buttons = new();

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(UtiltyFunctions.OverUI()) { return; }
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            clearUI();
        }
    }

    public void clearUI()
    {
        foreach(GameObject button in buttons)
        {
            Destroy(button);
        }
        buttons.Clear();
        gameObject.SetActive(false);
    }

    public void SetUI(BuildingBase building)
    {
        this.building = building;
        UnitScritableObject[] units = GameMaster.Instance.GetArmyAssetList(building.ArmyMaster.armyID).GetUnitsOfType(building.scritableObjectOfThisBuilding.unitConstuctionType);
        foreach(UnitScritableObject unit in units)
        {
            GameObject button = Instantiate(_unitButtonPrefab, _content);
            buttons.Add(button);
            UnitShopUIButton unitShopUIButton = button.GetComponent<UnitShopUIButton>();
            unitShopUIButton.SetUnit(unit, building);
        }
    }
}
