using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : ArmyActorBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetArmy(ArmyMaster armyMaster)
    {
        _army = armyMaster;
    }




    public void GiveOrder(GameObject obj, Vector3 position)
    {
        if(obj.layer == 3)
        {
            MakePathToPosition(position);
        }
        else
        {
            ArmyActorBase armyActor;
            if (TryGetComponent<ArmyActorBase>(out armyActor))
            {
                TargetEnemy(armyActor);
                return;
            }
            MakePathToPosition(position);
        }
    }


    public void TargetEnemy(ArmyActorBase actor)
    {

    }

    public void MakePathToPosition(Vector3 position)
    {
        Debug.Log(position);
    }




}
