using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Items : ScriptableObject
{
    public int id;
    public string itemName;
    public float itemDuration;
    public int cureValue;
    public int maxHealthUpgrade;
    public float moveSpeedBoost;
    public float boostDuration;
}