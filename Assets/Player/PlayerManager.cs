using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int _currentArmyIndex = 0;

    [SerializeField]
    private List<UnitBase> _units = new();
    [SerializeField]
    Vector2 _startpos;                                                                  //the starting position of a selection rect
    [SerializeField]
    Vector2 _endpos;                                                                    //the end position of a selection rect

    [SerializeField]
    private Texture selectionBoxImage;


    public int CurrentArmyIndex {get { return _currentArmyIndex; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnMouseUpdates();


    }


    void OnMouseUpdates()
    {
        if (BuildingSpawner.Instance.GetIsActive()) { return; }
        if (UtiltyFunctions.OverUI()) {
            if (Input.GetMouseButtonDown(0))
            {
                _units.Clear();
            }
            if (Input.GetMouseButtonUp(0))
            {
                _startpos = Vector2.zero;
                _endpos = Vector2.zero;
            }
            return; 
        }
        OnMouseLeft();
        OnMouseRight();
    }

    private void OnMouseRight()
    {
        if (Input.GetMouseButtonDown(1))
        {

            GameObject gameObject = UtiltyFunctions.GetObjectAtMousePoint();
            Vector3 point = UtiltyFunctions.getPosition();

            foreach (UnitBase unit in _units)
            {
                //Give Unit Right click info
                unit.GiveOrder(gameObject, point);
            }
        }
    }

    private void OnMouseLeft()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _units.Clear();
            _startpos = Input.mousePosition;
            GameObject gameObject = UtiltyFunctions.GetObjectAtMousePoint();
            ArmyActorBase armyActor = gameObject.GetComponent<ArmyActorBase>();
            if(armyActor == null) { return; }
            UnitSelection(armyActor);

            BuildingSelection(armyActor);
        }
        if (Input.GetMouseButtonUp(0))
        {
            getSelection();
        }
        if (Input.GetMouseButton(0))
        {
            _endpos = Input.mousePosition;
        }
    }

    private void BuildingSelection(ArmyActorBase armyActor)
    {
        BuildingBase building;
        try
        {
            building = (BuildingBase)armyActor;
        }
        catch
        {
            Debug.Log("not a building");
            return;
        }
        if(building.ArmyMaster != GameMaster.Instance.GetArmyByIndex(_currentArmyIndex)) { return; }
        if(building.scritableObjectOfThisBuilding.type != 0) { return; }
        //building UI pop up until you click on the back button or click anywhere on the screen that is not UI. the check for that should be in the UI code.
        UnitShopUIlist.Instance.gameObject.SetActive(true);
        UnitShopUIlist.Instance.SetUI(building);
    }
    private IEnumerator BuildingSelectionDelay(BuildingBase building)
    {
        yield return new WaitForEndOfFrame();

    }


    private void UnitSelection(ArmyActorBase armyActor)
    {
        UnitBase unit;
        try
        {
            unit = (UnitBase)armyActor;
        }
        catch
        {
            return;
        }
        if (unit.ArmyMaster != GameMaster.Instance.GetArmyByIndex(_currentArmyIndex)) { return; }
        //Add unit to the list of unites that should be controled until the player left clicks again.
        _units.Add(unit);
    }



    //makes a sellection between start and end pos and then add all units(that belong to the player) in that sellection to the units to give commands
    void getSelection()
    {
        Vector3 endpos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 startpos = Camera.main.ScreenToViewportPoint(_startpos);
        if (endpos == startpos) {
            _startpos = Vector2.zero;
            _endpos = Vector2.zero;

            return; }


        //vind the point closest to 0,0
        //calculate the size of the rectangle
        Rect r = CalcualteRectange(startpos,endpos);

        UnitBase[] controllers = GameObject.FindObjectsOfType<UnitBase>();

        foreach (UnitBase u in controllers)
        {
            if (!r.Contains(Camera.main.WorldToViewportPoint(u.transform.position))) { continue; }
            if (u.ArmyMaster != GameMaster.Instance.GetArmyByIndex(_currentArmyIndex)) { continue; }
            if (_units.Contains(u)) { continue; }
            _units.Add(u);
        }
        _startpos = Vector2.zero;
        _endpos = Vector2.zero;
    }

    private Rect CalcualteRectange(Vector2 a,Vector2 b)
    {
        Vector2 c = new Vector2();
        Vector2 d = new Vector2();
        //Moving the values around so that C is always the closest to 0,0 and D the furdest away.
        c.x = a.x < b.x ? a.x : b.x;
        c.y = a.y < b.y ? a.y : b.y;
        d.x = a.x > b.x ? a.x : b.x;
        d.y = a.y > b.y ? a.y : b.y;

        return new Rect(c.x, c.y, d.x - c.x, d.y - c.y);
    }


    void OnGUI()
    {
        if ( _startpos != Vector2.zero && _endpos != Vector2.zero)
        {
            GUI.DrawTexture(new Rect(_startpos.x, Screen.height - _startpos.y, _endpos.x - _startpos.x, -1 * ((Screen.height - _startpos.y) - (Screen.height - _endpos.y))), selectionBoxImage); //https://answers.unity.com/questions/601084/drawing-a-box-with-mouse-dragged-on-screen.html
        }
    }

}
