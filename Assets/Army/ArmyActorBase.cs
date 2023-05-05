using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyActorBase : MonoBehaviour, IPropagator
{
    [SerializeField]
    protected UtilityAI.AIBrain aIBrain;
    protected int _team;
    protected ArmyMaster _army;
    public ArmyMaster ArmyMaster { get { return _army; } }

    [SerializeField]
    protected float _health;
    [SerializeField]
    protected float _MaxHealth = 0;
    public float Health { get { return _health; } set { _health = Mathf.Clamp(value, 0, _MaxHealth); } }
    public float MaxHealth { get { return _MaxHealth; } set { if (_MaxHealth == 0) { _MaxHealth = value; }  } }

    protected Vector2I _gridPosition;
    public Vector2I GridPosition { get { return _gridPosition; } }

    [SerializeField]
    protected float influenceValue = 5;

    public float InfluenceValue { get { return influenceValue; } }

    [SerializeField]
    protected float armyInfluenceValue = 1;

    public float ArmyInfluenceValue { get { return armyInfluenceValue; } }

    public delegate void OndeathEvent();
    public OndeathEvent Ondeath;


    private void Awake()
    {
        aIBrain.SetNPC(this);
        SetNewGridPosition();
    }

    public virtual void DealDamage(float damage)
    {
        Health -= damage;
        if(Health == 0)
        {
            ArmyMaster?.OnActorDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public void SetNewGridPosition()
    {
        if(Grid.Instance == null) { return; }
        Node n = Grid.Instance.getSafeNodeFromWordlPoint(transform.position);
        _gridPosition = new Vector2I(n.X, n.Y);
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
