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
    public string enemyName;
    public int id;
    public enemyType type;
    public int enemyDamage;
    public int enemyHealth;
    public float enemySpeed;
    public float enemyFOV;
}
