using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer painting;
    [SerializeField] SpriteRenderer narration;

    [SerializeField] TextMeshProUGUI nerration; //Text Test
    string[] nerrations = { "숫자의 땅, 넘버랜드에서\n숲 속에 동물들이 평화롭게 살고 있는\n원 타운이라는 작은 마을이 있었습니다.\n이 곳의 주민들은 항상 서로 돌보면서\n지혜롭게 생활했습니다.\n 그 중에는 사랑하는 여동생 릴리와 함께\n생활하던 주인공도 있었습니다.",
        "어느 날 밤, 원 타운에\n마왕 마트라가 나타났습니다.\n마왕은 마을을 파괴하고\n릴리와 주민들을 잡아갔습니다.\n 주인공과 주민들이 무기를 들고\n싸워 보려고 했지만 마왕은\n숫자의 특별한 힘이 깃들어 있는\n카드를 다루는 능력이 없다면\n자기를 쓰러트릴 수 없다는 말을\n남기고 사라졌습니다.\n분노와 슬픔으로 가득 찬 주인공은\n어떤 대가를 치르더라도 여동생을\n되찾으리라고 맹세했습니다.",
        "주인공은 마왕이 말한\n카드에 대해 생각하고 있었습니다.\n그러던 어느 날, 주인공은\n사칙연산의 신이 찾아오는\n꿈을 꾸었습니다.\n신은 주인공의 마음 속에 있는\n분노와 결의를 보고 숫자의\n특별한 힘이 깃든 카드를\n다룰 수 있는 능력을 주었습니다.",
        "주인공은 화들짝 놀라 일어났습니다.\n그러자 허공에 숫자 카드들이\n떠 있는 것이 보였고 직감적으로\n주인공은 이것이 마왕이 말한\n마왕을 쓰러트릴 수 있는\n카드임을 알았습니다.\n그리고 이 카드들만으로는 마왕을\n쓰러트리기에는 부족하다고\n느낀 주인공은\n다른 카드들도 얻어 마왕을\n쓰러트리기 위해 모험을 떠납니다.", };

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
        nerration.text = nerrations[0];
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        painting.sprite = paintings[imageCount];
        narration.sprite = narratons[imageCount];
        nerration.text = nerrations[imageCount];

        if(imageCount == 3)
        {
            button.SetActive(true);
        }
    }
}
