using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;

    [SerializeField]
    private int attack;
    [SerializeField]
    private int defence;
}
