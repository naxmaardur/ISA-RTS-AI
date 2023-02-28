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
    }



    public void OnClick()
    {

    }

    public void OnRightClick()
    {

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




 
