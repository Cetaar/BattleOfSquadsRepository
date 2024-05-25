using UnityEngine;
public class DescriptionUnit: MonoBehaviour
{
    public string Description;
    public float Matter;
    public int ID;
    public float HP;
    public float Damage;
    public TypeDamage typeDamage;
    public float MaxDistance;
    public float MinDistance;
    public float Range; // ������ �����
    public float Recharge; // �����������
    public UnitType UnitType;
    public float ConstructionSpeed;
    public string Name;
    public GameObject[] PossibleActionsPrefabs;
    public GameObject Skeleton;
}
