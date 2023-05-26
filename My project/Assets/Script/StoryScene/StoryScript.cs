using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer painting;
    [SerializeField] SpriteRenderer narration;

    [SerializeField] TextMeshProUGUI nerration; //Text Test
    string[] nerrations = { "������ ��, �ѹ����忡��\n�� �ӿ� �������� ��ȭ�Ӱ� ��� �ִ�\n�� Ÿ���̶�� ���� ������ �־����ϴ�.\n�� ���� �ֹε��� �׻� ���� �����鼭\n�����Ӱ� ��Ȱ�߽��ϴ�.\n �� �߿��� ����ϴ� ������ ������ �Բ�\n��Ȱ�ϴ� ���ΰ��� �־����ϴ�.",
        "��� �� ��, �� Ÿ�\n���� ��Ʈ�� ��Ÿ�����ϴ�.\n������ ������ �ı��ϰ�\n������ �ֹε��� ��ư����ϴ�.\n ���ΰ��� �ֹε��� ���⸦ ���\n�ο� ������ ������ ������\n������ Ư���� ���� ���� �ִ�\nī�带 �ٷ�� �ɷ��� ���ٸ�\n�ڱ⸦ ����Ʈ�� �� ���ٴ� ����\n����� ��������ϴ�.\n�г�� �������� ���� �� ���ΰ���\n� �밡�� ġ������ ��������\n��ã������� �ͼ��߽��ϴ�.",
        "���ΰ��� ������ ����\nī�忡 ���� �����ϰ� �־����ϴ�.\n�׷��� ��� ��, ���ΰ���\n��Ģ������ ���� ã�ƿ���\n���� �پ����ϴ�.\n���� ���ΰ��� ���� �ӿ� �ִ�\n�г�� ���Ǹ� ���� ������\nƯ���� ���� ��� ī�带\n�ٷ� �� �ִ� �ɷ��� �־����ϴ�.",
        "���ΰ��� ȭ��¦ ��� �Ͼ���ϴ�.\n�׷��� ����� ���� ī�����\n�� �ִ� ���� ������ ����������\n���ΰ��� �̰��� ������ ����\n������ ����Ʈ�� �� �ִ�\nī������ �˾ҽ��ϴ�.\n�׸��� �� ī��鸸���δ� ������\n����Ʈ���⿡�� �����ϴٰ�\n���� ���ΰ���\n�ٸ� ī��鵵 ��� ������\n����Ʈ���� ���� ������ �����ϴ�.", };

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
