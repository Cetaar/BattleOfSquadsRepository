using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materia : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text materiaString;
    [SerializeField] int PrimaryMaterial;
    [SerializeField] string NotEnoughMatter;
    public static Materia instance;
    [SerializeField] private float _materiaValue;
    public float MateriaValue 
    {
        get
        {
            return _materiaValue;

        }
        set
        {

            _materiaValue = value;
            MaterialIntoIU();
        }
    }
    public void NoMaterias()
    {
        MainCanvas.Warning.GetComponent<ForWarning>().WarningOutput(NotEnoughMatter);
    }
    public static bool MinusMatter(float Munus)
    {
        if (instance.MateriaValue - Munus >= 0)
        {
            instance.MateriaValue -= Munus;
            instance.MaterialIntoIU();
            return true;
        }
        else
        {
            instance.NoMaterias();
            return false;
        }
    }

    public static bool PlusMatter(float Plus)
    {
        instance.MateriaValue += Plus;
        instance.MaterialIntoIU();
        return true;
    }

    public void MaterialIntoIU()
    {
        materiaString.text = ((int)MateriaValue).ToString();
    }
    private void Start()
    {
        instance = this;
        MateriaValue = PrimaryMaterial;
        MaterialIntoIU();
    }
}
