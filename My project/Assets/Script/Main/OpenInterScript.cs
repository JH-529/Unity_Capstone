using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenInterScript : MonoBehaviour
{
    [SerializeField] int interLevel;
    [SerializeField] Button button;
    [SerializeField] GameObject BGimage;
    [SerializeField] GameObject OpenPopUp;
    [SerializeField] TextMeshProUGUI OpenPopUpText;    

    void Start()
    {
        button.interactable = false;
        BGimage.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.playerLevel >= interLevel)
        {
            button.interactable = true;
        }
    }

    public void OpenIntermediate()
    {
        if (BGimage.activeSelf == false)
        {
            Vector3 pos = OpenPopUp.transform.localPosition;
            pos.x = 0;
            pos.y = 105;
            OpenPopUp.transform.localPosition = pos;

            OpenPopUpText.text = "이미 다음 구역은 열려있어! \n 어서 모험을 떠나자!";
            return;
        }

        BGimage.SetActive(false);

        Vector3 position = OpenPopUp.transform.localPosition;
        position.x = 0;
        position.y = 105;
        OpenPopUp.transform.localPosition = position;

        OpenPopUpText.text = "다음 지역 오픈!";
    }

}
