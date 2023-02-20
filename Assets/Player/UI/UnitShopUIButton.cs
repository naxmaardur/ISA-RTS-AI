using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UnitShopUIButton : MonoBehaviour
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
}
