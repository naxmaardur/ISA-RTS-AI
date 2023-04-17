using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyActorBase : MonoBehaviour
{
    [SerializeField]
    protected UtilityAI.AIBrain aIBrain;
    protected int _team;
    protected ArmyMaster _army;
    public ArmyMaster ArmyMaster { get { return _army; } }

    protected float _health;
    public float Health { get { return _health; } set { _health = Mathf.Clamp(value, 0, 5000); } }

    public virtual void DealDamage(float damage)
    {
        Health -= damage;
        if(Health == 0)
        {
            ArmyMaster?.OnActorDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
