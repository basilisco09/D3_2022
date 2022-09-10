using UnityEngine;

public enum boostType
{
  healthy,
  speed,
  damage,
  ammo,
  none
}
public enum debuffType
{
  healthy,
  speed,
  damage,
  attackRange,
  viewRange,
  none
}

[CreateAssetMenu (menuName = "Item")] 
public class Items : ScriptableObject
{
  public int id;
  public string itemName;
  public Sprite itemIcon;
  public int level;
  public int itemDurability;
  public float successRating;
  public int inventorySlots;
  public boostType boost;
  public debuffType debuf;
}