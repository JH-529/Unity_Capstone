using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;

    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }

    void Awake()
    {
        FreshSlot();
        GameObject mySelf = GameObject.Find("Inventory");
        mySelf.SetActive(false);
    }

    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }
        for(; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }

    public void AddItem(Item _item)
    {
        if(items.Count < slots.Length)
        {
            items.Add(_item);
            FreshSlot();
        }
        else
        {
            Debug.Log("슬롯이 가득 참!");
        }
    }

    public void DeleteRandomItem()
    {
        if(items.Count > 0)
        {            
            int rand = Random.Range(0, items.Count);
            Debug.Log("임의의 " + items[rand].itemName + " 제거!");
            items.RemoveAt(rand);
            FreshSlot();
            
        }
        else
        {
            Debug.Log("인벤토리에 아이템이 없음!");
        }
    }

    public void DeleteItem(string name)
    { 
        for(int i=0; i<items.Count; i++)
        {
            Debug.Log(i.ToString() + "번째: " + items[i].itemName);

            if (items[i].itemName.Equals(name))
            {
                items.RemoveAt(i);
                FreshSlot();
                Debug.Log(name + " 제거!");
            }
        }
    }

}
