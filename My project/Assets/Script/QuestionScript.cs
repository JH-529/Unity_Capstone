using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionScript : MonoBehaviour
{
    public SpriteRenderer qRenderer;
    public Sprite[] sprites;
    public Sprite right;
    public Sprite[] wrong;

    void Start()
    {         
        qRenderer = GameObject.Find("Question").GetComponent<SpriteRenderer>();

        if (qRenderer.sprite.name.Equals("Question1"))
        {
            Debug.Log(qRenderer.sprite.name);
            sprites = Resources.LoadAll<Sprite>("Sprites/Answer1");
            OptionSetting(sprites);
        }

        if (qRenderer.sprite.name.Equals("Question2"))
        {
            Debug.Log(qRenderer.sprite.name);
            sprites = Resources.LoadAll<Sprite>("Sprites/Answer2");
            OptionSetting(sprites);
        }

        if (qRenderer.sprite.name.Equals("Question3"))
        {
            Debug.Log(qRenderer.sprite.name);
            sprites = Resources.LoadAll<Sprite>("Sprites/Answer3");
            OptionSetting(sprites);
        }

        if (qRenderer.sprite.name.Equals("Question4"))
        {
            Debug.Log(qRenderer.sprite.name);
            sprites = Resources.LoadAll<Sprite>("Sprites/Answer4");
            OptionSetting(sprites);
        }
    }
    
    public void Right()
    {
        Debug.Log("정답!");
        GameManager.playerStatus.hp += 10;
        GameManager.playerGold += 30;
        if(GameManager.playerStatus.hp > GameManager.playerStatus.maxHp)
        { GameManager.playerStatus.hp = GameManager.playerStatus.maxHp; }
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }
    public void Wrong()
    {
        Debug.Log("오답!!");
        GameManager.playerGold -= 20;
        if(GameManager.playerGold < 0)
        { GameManager.playerGold = 0; }
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    public void OptionSetting(Sprite[] sprites)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Option");
        Button[] buttons = new Button[sprites.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = gameObjects[i].GetComponent<Button>();
        }

        right = GetRightSprite(sprites);
        wrong = GetWrongSprite(sprites);

        // 임의의 버튼을 정답으로 설정
        int rand = Random.Range(0, sprites.Length);
        gameObjects[rand].GetComponent<Image>().sprite = right;
        buttons[rand].onClick.AddListener(Right);

        // 정답을 제외한 버튼을 오답으로 설정
        int wrongCount = 0;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (i != rand)
            {
                gameObjects[i].GetComponent<Image>().sprite = wrong[wrongCount];
                buttons[i].onClick.AddListener(Wrong);
                wrongCount++;
            }
        }
    }
    Sprite GetRightSprite(Sprite[] sprites)
    {
        for(int i=0; i<sprites.Length;i++) 
        {
            if (sprites[i].name.Equals("Right"))
            { return sprites[i]; }
        }
        return null;
    }
    Sprite[] GetWrongSprite(Sprite[] sprites)
    {
        Sprite[] rSprites = new Sprite[sprites.Length - 1];

        int spriteCount = 0;
        for (int i = 0; i < sprites.Length; i++)
        {
            string spriteName = sprites[i].name;
            spriteName = spriteName.Substring(0, 5);

            Debug.Log(spriteName);
            if (spriteName.Equals("Wrong"))
            {
                rSprites[spriteCount] = sprites[i];
                spriteCount++;
            }
        }

        return rSprites;
    }
}
