using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForUserMenu : MonoBehaviour
{
    [SerializeField] private GameObject _allSelectedObjects;
    [SerializeField] private GameObject _selectedObject;

    [SerializeField] private List<TypeOfUnit> _objects;
    private bool flag;

    [SerializeField] private GameObject ButtonPrefab;

    private GameObject CreatedObject;


    public void SetAllSelectedObjectsIU()
    {
        CleaningFromChildObjects(_allSelectedObjects);
        MainCanvas.PossibleActions.GetComponent<ManagerPossibleActions>().ClearActions();

        _objects.Clear();

        foreach (ISelect unit in ObjectSelect.instance.selectObjects)
        {
            DescriptionUnit ObjectNow = ((Component)unit).GetComponent<DescriptionUnit>();
            if (ObjectNow != null)
            {
                bool found = false;
                foreach (TypeOfUnit obj in _objects)
                {
                    if (ObjectNow.Name == obj.CommonName)
                    {
                        obj.units.Add(unit);
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    TypeOfUnit newObject = new TypeOfUnit
                    {
                        CommonName = ObjectNow.Name,
                        descriptionUnit = ObjectNow
                    };
                    newObject.units.Add(unit);
                    _objects.Add(newObject);
                }
            }
        }

        foreach (TypeOfUnit obj in _objects)
        {
            CreatedObject = Instantiate(ButtonPrefab, _allSelectedObjects.transform);
            CreatedObject.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = obj.CommonName;
            CreatedObject.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = obj.units.Count.ToString();
            CreatedObject.GetComponent<ForButtonForAllSelectedObject>().descriptionUnit = obj.descriptionUnit;
            CreatedObject.GetComponent<ForButtonForAllSelectedObject>().selectedGroup = obj.units;
        }
    }


    private void CleaningFromChildObjects(GameObject Parent)
    {
        foreach (Transform child in Parent.transform)
        {
            child.GetComponent<ForButtonForAllSelectedObject>().Clear();
            Destroy(child.gameObject);
        }
    }
}


[System.Serializable]
public class TypeOfUnit
{
    public string CommonName = "";
    public DescriptionUnit descriptionUnit;
    public List<ISelect> units = new List<ISelect>();
}