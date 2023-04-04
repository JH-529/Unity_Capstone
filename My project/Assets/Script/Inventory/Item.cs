using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;

    [SerializeField]
    public int attack;
    [SerializeField]
    public int defence;
}
