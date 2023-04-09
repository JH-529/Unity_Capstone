using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject[] icons;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("Sprites/Shop");
        icons = GameObject.FindGameObjectsWithTag("ShopIcon");

        int rand = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[rand];
    }

    void Start()
    {
        int rand = 0;
                
        // Debug.Log(gameObject.name); // 현재 오브젝트의 이름 출력

        // 상점 아이템이 겹치지 않게 비교하면서 생성
        for (int i = 0; i < icons.Length; i++)
        {
            SpriteRenderer sRenderer = GetSprite(icons[i]);
            
            // Debug.Log(i + 1 + "번째 아이콘: " + sRenderer.sprite.name + "생성된 아이콘: " + sprites[rand].name);
            // 랜덤으로 골라온 Icon과 동일한 Icon이 적용된 상점 Icon이 있다면 다시 rand 생성
            if (sRenderer.sprite.name == sprites[rand].name)
            {
                InfiniteLoopDetector.Run();
                // Debug.Log("비교됨");
                i = -1;
                rand = Random.Range(0, sprites.Length);
                continue;
            }            
        }

        spriteRenderer.sprite = sprites[rand];
    }

    // SpriteRenderer 반환 함수
    SpriteRenderer GetSprite(GameObject gameObject)
    {
        return gameObject.GetComponent<SpriteRenderer>();
    }
}
