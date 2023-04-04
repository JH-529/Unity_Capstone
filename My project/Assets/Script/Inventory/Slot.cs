using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] GameObject slotTextWindow;
    TextMeshProUGUI slotText;
    string explanation;

    private Item _item;
    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;
            if(_item != null)
            {
                image.sprite = item.itemImage;
                image.color = new Color(1, 1, 1, 1);
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }

    void Start()
    {
        slotTextWindow.SetActive(false);
        slotText = slotTextWindow.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();        
    }

    private void OnMouseEnter()
    {        
        if(item)
        {
            Debug.Log("아이템: " + item.itemName);
            slotTextWindow.SetActive(true);
            explanation = "이름: " + item.itemName + "\n공격력: " + item.attack.ToString() + "\n방어력: " + item.defence.ToString();
            slotText.text = explanation;
        }        
    }

    private void OnMouseExit()
    {        
        slotTextWindow.SetActive(false);
    }

}
