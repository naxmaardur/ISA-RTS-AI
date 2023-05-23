using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class UtiltyFunctions 
{

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    //returns  the direction between two vectors
    public static Vector3 Vector3Direction(Vector3 from, Vector3 to)
    {
        return (to - from).normalized;
    }

    //returns the current position of the mouse in world space aligned with the ground
    public static Vector3 getPosition()
    {
        Vector3 MousePotion = Input.mousePosition;
        MousePotion.z = 100.0f;
        MousePotion = Camera.main.ScreenToWorldPoint(MousePotion);
        RaycastHit hit;
        Vector3 v3 = new Vector3();
        if (Physics.Raycast(Camera.main.transform.position, Vector3Direction(Camera.main.transform.position, MousePotion), out hit, Mathf.Infinity, GameMaster.Instance.GroundLayer))
        {
            v3 = hit.point;
        }

        return v3;
    }

    //Check if the mouse is over the map
    public static bool OverMap()
    {
        Vector3 MousePotion = Input.mousePosition;
        MousePotion.z = 100.0f;
        MousePotion = Camera.main.ScreenToWorldPoint(MousePotion);
        if (Physics.Raycast(Camera.main.transform.position, Vector3Direction(Camera.main.transform.position, MousePotion), Mathf.Infinity, GameMaster.Instance.GroundLayer))
        {
            return true;
        }

        return false;
    }

    public static GameObject GetObjectAtMousePoint()
    {
        Vector3 MousePotion = Input.mousePosition;
        MousePotion.z = 100.0f;
        MousePotion = Camera.main.ScreenToWorldPoint(MousePotion);
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Vector3Direction(Camera.main.transform.position, MousePotion),out hit, Mathf.Infinity))
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    //returns the point of the grid map the mouse is over 
    public static Vector3 getGridPointOnMap(float gridSize = 1)
    {
        Vector3 v3 = getPosition();
        v3.x = Mathf.Round(Mathf.Round(v3.x) / gridSize) * gridSize;
        v3.z = Mathf.Round(Mathf.Round(v3.z) / gridSize) * gridSize;
        return v3;
    }

    //checks if the mouse is over ui or not
    public static bool OverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject.GetComponent<MouseUIClickthrough>() != null)
            {
                raycastResultList.RemoveAt(i);
                i--;
            }
        }

        return raycastResultList.Count > 0;
    }



}
