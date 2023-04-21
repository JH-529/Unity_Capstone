using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer painting;
    [SerializeField] SpriteRenderer narration;
    [SerializeField] Sprite[] paintings;
    [SerializeField] Sprite[] narratons;

    [SerializeField] GameObject button;
    public static int imageCount = 0;
    public static int bookPage = 0;

    // Start is called before the first frame update
    void Awake()
    {
        paintings = Resources.LoadAll<Sprite>("Book/Painting");
        narratons = Resources.LoadAll<Sprite>("Book/Narration");
        bookPage = paintings.Length;
    }

    void Start()
    {
        painting.sprite = paintings[0];
        narration.sprite = narratons[0];
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        painting.sprite = paintings[imageCount];
        narration.sprite = narratons[imageCount];

        if(imageCount == 3)
        {
            button.SetActive(true);
        }
    }
}
