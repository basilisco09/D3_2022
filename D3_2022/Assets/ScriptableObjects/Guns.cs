using UnityEngine;

[CreateAssetMenu(menuName = "Gun")]
public class Guns : ScriptableObject
{
    public int id;
    public string gunName;
    public int gunDamage;
    public GameObject bullet;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public Sprite gunSprite;

}
