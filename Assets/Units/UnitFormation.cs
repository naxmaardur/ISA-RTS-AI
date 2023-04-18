using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFormation : MonoBehaviour
{
    [SerializeField]
    private Transform[] FormationPositions;


    [SerializeField]//for testing or potential chalange levels a manual formation can be made.
    private List<UnitBase> units;




    public void AddUnit(UnitBase unit)
    {
        units.Add(unit);
        UpdateFormation();
    }


    public void RemoveUnit(UnitBase unit)
    {
        units.Remove(unit);
        UpdateFormation();
    }


    private void UpdateFormation()
    {
        int i = 0;
        foreach(UnitBase unit in units)
        {
            unit.SetTransformDestination(FormationPositions[i]);
            i++;
        }
    }


    private void InitFormation()
    {
        foreach (UnitBase unit in units)
        {
            unit.SetFormation(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitFormation();
        UpdateFormation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
