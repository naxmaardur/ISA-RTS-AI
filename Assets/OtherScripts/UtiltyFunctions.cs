using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtiltyFunctions 
{


    //returns  the direction between two vectors
    static Vector3 Vector3Direction(Vector3 from, Vector3 to)
    {
        return (to - from).normalized;
    }

    //returns the current position of the mouse in world space aligned with the ground
    public static Vector3 getPosition()
    {
        Vector3 MousePotion = Input.mousePosition;
        MousePotion = Camera.current.ScreenToWorldPoint(MousePotion);
        RaycastHit hit;
        Vector3 v3 = new Vector3();
        if (Physics.Raycast(MousePotion, Camera.current.transform.forward, out hit, Mathf.Infinity, GameMaster.Instance.GroundLayer))
        {
            v3 = hit.point;
        }

        return v3;
    }

    //returns the point of the grid map the mouse is over 
    public static Vector3 getGridPointOnMap(float gridSize = 1)
    {
        Vector3 v3 = getPosition();
        v3.x = Mathf.Round(Mathf.Round(v3.x) / gridSize) * gridSize;
        v3.z = Mathf.Round(Mathf.Round(v3.z) / gridSize) * gridSize;
        return v3;
    }



}
