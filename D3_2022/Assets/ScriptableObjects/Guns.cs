using UnityEngine;

[CreateAssetMenu(menuName = "Gun")]
public class Guns : ScriptableObject
{
    public int id;
    public string gunName;
    public int gunDamage;
    [Tooltip ("The magazine size is in bullets.")] 
    public int magazineSize;
    [Tooltip ("The cooldown time is in seconds")] 
    public float cooldownTime;
    [Tooltip ("The reload time is in seconds")] 
    public float reloadTime;
    public GameObject bullet;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public Sprite gunSprite;

}
