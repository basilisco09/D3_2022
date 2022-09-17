using UnityEngine;

public enum type
{
  consumible,
  ammo,
  weapon
}


[CreateAssetMenu(menuName = "Item")]
public class Items : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite itemIcon;
    public int itemDurability;
    public type type;
}