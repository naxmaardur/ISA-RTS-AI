using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;

public class ArmyControlAI : ControlerAI
{
    BaseAI baseAI; //AI in control of base decisions
    [SerializeField]
    AIBrainControler AIBrain;
    List<UnitFormation> formations0 = new();
    List<UnitFormation> formations1 = new();
    List<UnitFormation> formations2 = new();

    [SerializeField]
    GameObject formationPrefab;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void SetUpAIApponent(ArmyMaster army, ArmyAssetList armyAssets)
    {
        base.army = army;
        this.armyAssets = armyAssets;
        army.OnActorDestroyed += OnActorDestroyed;
        army.OnUnitAdded += OnUnitAdded;
    }

    private void OnActorDestroyed(ArmyActorBase actor)
    {
        RemoveEmptyFormations(ref formations0);
        RemoveEmptyFormations(ref formations1);
        RemoveEmptyFormations(ref formations2);
    }

    private void OnUnitAdded(UnitBase unit)
    {
        switch (unit.unitScritable.type)
        {
            case 0:
                AddUnitToFormation(unit, ref formations0);
                break;
            case 1:
                AddUnitToFormation(unit, ref formations1);
                break;
            case 2:
                AddUnitToFormation(unit, ref formations2);
                break;
        }
    }


    void RemoveEmptyFormations(ref List<UnitFormation> formations)
    {
        List<UnitFormation> toRemove = new();
        foreach(UnitFormation formation in formations)
        {
            if(formation.FormationSize == 0)
            {
                toRemove.Add(formation);
            }
        }

        foreach(UnitFormation formation1 in toRemove)
        {
            formations.Remove(formation1);
        }
    }

    void AddUnitToFormation(UnitBase unit, ref List<UnitFormation> formations)
    {
        for(int i = 0; i < formations.Count; i++)
        {
            if(formations[i].FormationMaxSize > formations[i].FormationSize)
            {
                formations[i].AddUnit(unit);
                unit.SetFormation(formations[i]);
                return;
            }
        }

        GameObject g = Instantiate(formationPrefab, unit.transform.position, Quaternion.identity);
        UnitFormation formation = g.GetComponent<UnitFormation>();
        unit.SetFormation(formation);
        formation.AddUnit(unit);
        formations.Add(formation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

}
