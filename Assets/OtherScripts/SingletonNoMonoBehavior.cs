using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonNoMonoBehavior<T>
{
    private static T _instance;
    public static T Instance { get { return _instance; } set { if (_instance == null) { _instance = value; } else { Debug.LogError("more then one Instance for singleton"); } } }
}
