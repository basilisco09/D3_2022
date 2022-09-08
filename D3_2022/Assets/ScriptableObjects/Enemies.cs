using UnityEngine;

public enum enemyType
{
    agile,
    standard,
    tank
}

[CreateAssetMenu(menuName = "Enemy")]
public class Enemies : ScriptableObject
{
    public enemyType type;
    public int enemyDamage;
    public float enemySpeed;
}
