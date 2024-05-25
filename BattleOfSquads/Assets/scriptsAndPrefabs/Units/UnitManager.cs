using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private List<Unit>[] Units;
    [SerializeField] private List<UnitDead> UnitsDead;
    public static UnitManager instance;


    private int TeamCount;
    private void Awake()
    {
        instance = this;
        TeamCount = DescriptionCommand.Commands.Count;
        Units = new List<Unit>[TeamCount]; //TeamCount количество строк

        for (int i = 0; i < TeamCount; i++)
        {
            Units[i] = new List<Unit>();
        }
    }
    public void AddUnits(Unit unit)
    {
        foreach (Command command in DescriptionCommand.Commands)
        {
            if (command.Number == int.Parse(LayerMask.LayerToName(unit.gameObject.layer)))
            {
                Units[command.Number - 1].Add(unit);
            }
        }
    }
    public void RemoveUnits(Unit unit)
    {
        foreach (Command command in DescriptionCommand.Commands)
        {
            if (command.Number == int.Parse(LayerMask.LayerToName(unit.gameObject.layer)))
            {
                Units[command.Number - 1].Remove(unit);
            }
        }
    }
    public Unit[] GetAllUnits(int i)
    {
        return Units[i-1].ToArray();
    }

    public void AddUnitsDead(UnitDead unitDead)
    {
        UnitsDead.Add(unitDead);
    }
    public void RemoveUnitsDead(UnitDead unitDead)
    {
        UnitsDead.Remove(unitDead);
    }
    public UnitDead[] GetAllUnitsDead()
    {
        return UnitsDead.ToArray();
    }
}
