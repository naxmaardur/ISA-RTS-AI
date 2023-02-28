using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBuilding : BuildingBase
{
    [SerializeField]
    private int _moneyPerSecond;


    // Start is called before the first frame update
    void Start()
    {
        PlacedEvent += StartTick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartTick()
    {
        StartCoroutine(MoneyTick());
    }

    private IEnumerator MoneyTick()
    {
        yield return new WaitForSeconds(1);
        _army.AddMoney(_moneyPerSecond);
    }
}
