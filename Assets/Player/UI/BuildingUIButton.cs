using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingUIButton : MonoBehaviour
{
    public BuildingScriptableObject building;
    public Button button;
    private ArmyMaster _player;
    [SerializeField]
    private TextMeshProUGUI _costText;

    private void Start()
    {
        _player = GameMaster.Instance.GetPlayer();
        _player.OnMoneyUpdated += OnMoneyUpdated;
    }

    public void AssignBuildingToButton(BuildingScriptableObject building)
    {
        this.building = building;
        _costText.text = ""+building.cost;
    }


    private void OnEnable()
    {
        if (_player != null)
        {
            _player.OnMoneyUpdated += OnMoneyUpdated;
        }
    }

    private void OnDisable()
    {
        if (_player != null)
        {
            _player.OnMoneyUpdated -= OnMoneyUpdated;
        }
    }

    public void AssignBuildingToSpawner()
    {
        BuildingSpawner.Instance.SetBuilding(building);
    }

    private void OnMoneyUpdated(int money)
    {
        if(money < building.cost)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
}
