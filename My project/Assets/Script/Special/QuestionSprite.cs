using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("Sprites/Questions");

        int rand = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[rand];
        InfiniteLoopDetector.Run();
    }
}
