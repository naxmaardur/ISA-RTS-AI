using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class UnitShopUIButton : MonoBehaviour, IPointerClickHandler
{
    private BuildingBase _building;
    private UnitScritableObject _unit;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI queText;

    public int UnitsUnderConstruction;

    public UnitScritableObject Unit { get { return _unit; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void SetUnit(UnitScritableObject unit,BuildingBase building)
    {
        _building = building;
        _unit = unit;
        costText.text = ""+unit.cost;
        timeText.text = ""+unit.constructTime;
        attackText.text = "" + unit.attack;
        rangeText.text = "" + unit.range;

        _building.SubToUIElement(this);
        UpdateCount(_building.GetQueueCountOfUnit(_unit));

    }

    public void UpdateCount(int count)
    {
        UnitsUnderConstruction = count;
        if (count == 0)
        {
            queText.text = "";
        }
        else
        {
            queText.text = "" + UnitsUnderConstruction;
        }
    }



    public void OnClick()
    {
        //check if we have money and then add the unit to the buildings que
        if(_building.ArmyMaster.Money > _unit.cost) { return; }
        _building.AddUnitToQueue(_unit);
        UpdateCount(++UnitsUnderConstruction);
        _building.ArmyMaster.RemoveMoney(_unit.cost);
    }

    public void OnRightClick()
    {
        //check if a unit is being made and then remove it from the que and return the money
        if(_building.GetQueueCountOfUnit(_unit) <= 0) { return; }
        UpdateCount(--UnitsUnderConstruction);
        _building.RemoveFromQueue(_unit);
        _building.ArmyMaster.AddMoney(_unit.cost);


    }

    private void OnDisable()
    {
        //unsub
        _building.RemoveSubToUIElement(this);
    }


    public UnityEvent leftClick;
    public UnityEvent middleClick;
    public UnityEvent rightClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Middle)
            middleClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            rightClick.Invoke();
    }
}




 
