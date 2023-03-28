using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum operators
{
    PLUS,
    MINUS,
    MULTIPLY,
    DIVIDE,
}

public class SpriteManager : MonoBehaviour
{
    //public Sprite[] numberSprites = new Sprite[10];
    //public Sprite[] operatorSprites = new Sprite[4];
    //public GameObject target;
    //public Image image;
    public GameObject[] selectedCards;
    public Sprite sprite;
    public Sprite[] sprites;
    public Image image;
    public Image target;
    public int[] playerNumbers = new int[5];

    public void Start()
    {
        selectedCards = GameObject.FindGameObjectsWithTag("SelectedCard");
        image = GetComponent<Image>();
        sprites = Resources.LoadAll<Sprite>("Sprites/Numbers");
    }

    public void InsertNumber(int count, int value)
    {
        playerNumbers[count] = value;
        image.sprite = sprites[count];
    }

    public void GetBtn()
    {
        GameObject tempBtn = EventSystem.current.currentSelectedGameObject;

        if(tempBtn.name == "Card1")
        {
            target = selectedCards[0].GetComponent<Image>();
            target.sprite = image.sprite;
        }

        if (tempBtn.name == "Card2")
        {
            target = selectedCards[1].GetComponent<Image>();
            target.sprite = image.sprite;
        }

        if (tempBtn.name == "Card3")
        {
            target = selectedCards[2].GetComponent<Image>();
            target.sprite = image.sprite;
        }
    }
}
