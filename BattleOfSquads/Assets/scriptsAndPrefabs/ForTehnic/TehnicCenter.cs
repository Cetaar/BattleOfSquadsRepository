using UnityEngine;
using TMPro;

public class TehnicCenter : MonoBehaviour
{
    [SerializeField] private string _nameTag;
    private GameObject _pastObject;// default - Place

    private DescriptionUnit _descriptionUnit;

    private OutputTextWithVariables _descriptionCharacteristics;

    private void Start()
    {
        _descriptionCharacteristics = GameObject.Find("DescriptionCharacteristics").GetComponent<OutputTextWithVariables>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Nowhit;
            if (Physics.Raycast(ray, out Nowhit))
            {
                if (Nowhit.transform.tag == _nameTag)
                {
                    if (!Nowhit.transform.GetComponent<ForUnitTehnic>().IsSelect)
                    {
                        if (_pastObject != null)
                        {
                            FalseForIsObject();
                            OutputDescriptionUnit();
                        }
                        _descriptionUnit = Nowhit.transform.GetComponent<DescriptionUnit>();
                        Nowhit.transform.GetComponent<ForUnitTehnic>().IsSelect = true;
                        Nowhit.transform.position = transform.position;
                        _pastObject = Nowhit.transform.gameObject;
                        OutputDescriptionUnit();
                    }
                }
            }
        }
    }
    public void FalseForIsObject()
    {
        _pastObject.GetComponent<ForUnitTehnic>().IsSelect = false;
        _pastObject.transform.position = _pastObject.GetComponent<ForUnitTehnic>().DefaultPlace;
        _descriptionCharacteristics.Hide();
        _pastObject = null;
    }
    private void OutputDescriptionUnit()
    {
        _descriptionCharacteristics.Output(
            _descriptionUnit.Description,

            _descriptionUnit.Matter,
            _descriptionUnit.HP,
            _descriptionUnit.Damage,
            _descriptionUnit.typeDamage,
            _descriptionUnit.MaxDistance,
            _descriptionUnit.MinDistance,
            _descriptionUnit.Range,
            _descriptionUnit.Recharge,
            _descriptionUnit.ConstructionSpeed
            );
    }
}
