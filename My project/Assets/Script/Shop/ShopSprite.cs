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
                
        // Debug.Log(gameObject.name); // ���� ������Ʈ�� �̸� ���

        // ���� �������� ��ġ�� �ʰ� ���ϸ鼭 ����
        for (int i = 0; i < icons.Length; i++)
        {
            SpriteRenderer sRenderer = GetSprite(icons[i]);
            
            // Debug.Log(i + 1 + "��° ������: " + sRenderer.sprite.name + "������ ������: " + sprites[rand].name);
            // �������� ���� Icon�� ������ Icon�� ����� ���� Icon�� �ִٸ� �ٽ� rand ����
            if (sRenderer.sprite.name == sprites[rand].name)
            {
                InfiniteLoopDetector.Run();
                // Debug.Log("�񱳵�");
                i = -1;
                rand = Random.Range(0, sprites.Length);
                continue;
            }            
        }

        spriteRenderer.sprite = sprites[rand];
    }

    // SpriteRenderer ��ȯ �Լ�
    SpriteRenderer GetSprite(GameObject gameObject)
    {
        return gameObject.GetComponent<SpriteRenderer>();
    }
}
