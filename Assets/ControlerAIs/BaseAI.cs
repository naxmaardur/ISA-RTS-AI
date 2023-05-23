using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;

public class BaseAI : ControlerAI
{
    [SerializeField]
    AIBrainControler aIBrain;
    // Start is called before the first frame update
    void Start()
    {
        aIBrain.SetNPC(this);
        StartCoroutine(delayedUpdates());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator delayedUpdates()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            aIBrain.DecideBestAction();
        }
    }
}
