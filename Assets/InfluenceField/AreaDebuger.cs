using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreaDebuger : MonoBehaviour
{
    public bool DrawGizmos;
    public GameObject DebugUIPreFab;
    List<TextMeshProUGUI> displays = new();


    // Start is called before the first frame update
    void Start()
    {
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(90, 0, 0);
        for (int x = 0; x < Grid.Instance.Areas.Length; x++)
        {
            GameObject g = Instantiate(DebugUIPreFab,transform);
            g.transform.position = Grid.Instance.Areas[x].Center + (Vector3.up * 60);
            g.transform.rotation = rotation;
            g.name = ""+x;
            TextMeshProUGUI textMeshPro = g.GetComponentInChildren<TextMeshProUGUI>();
            displays.Add(textMeshPro);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int x = 0; x < Grid.Instance.Areas.Length; x++)
        {
            displays[x].text = ""+Grid.Instance.Areas[x].ArmyStrength;
        }
    }



    private void OnDrawGizmos()
    {
        if(!DrawGizmos) { return; }
        if (Grid.Instance == null) { return; }
        if (Grid.Instance.Areas == null) { return; }
        int i= -1;
        foreach (NodeArea area in Grid.Instance.Areas)
        {
            i++;
            if (area.ArmyStrength == 0) { continue; }
            if(area.ArmyStrength > 0)
            {
                Gizmos.color = new Color(1, 0, 0, 1 * area.ArmyStrength);
            }else
            {
                Gizmos.color = new Color(0, 0, 1, 1 * Mathf.Abs(area.ArmyStrength));
            }
            foreach (Node n in area.Nodes)
            {
                Gizmos.DrawCube(n._worldPoint + (Vector3.up * i), new Vector3(0.9f, 0.9f, 0.9f));
            }
        }
    }
}
