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
            Debug.Log("������ ���� ��!");
        }
    }

    public void DeleteRandomItem()
    {
        if(items.Count > 0)
        {            
            int rand = Random.Range(0, items.Count);
            Debug.Log("������ " + items[rand].itemName + " ����!");
            items.RemoveAt(rand);
            FreshSlot();
            
        }
        else
        {
            Debug.Log("�κ��丮�� �������� ����!");
        }
    }

    public void DeleteItem(string name)
    { 
        for(int i=0; i<items.Count; i++)
        {
            Debug.Log(i.ToString() + "��°: " + items[i].itemName);

            if (items[i].itemName.Equals(name))
            {
                items.RemoveAt(i);
                FreshSlot();
                Debug.Log(name + " ����!");
            }
        }
    }

}
