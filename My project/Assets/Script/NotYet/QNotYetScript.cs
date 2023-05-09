using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QNotYetScript : MonoBehaviour
{
    [SerializeField] int interGold;
    [SerializeField] GameObject notYetQ;
    [SerializeField] GameObject popUp;
    [SerializeField] TextMeshProUGUI popText;
      
    // Update is called once per frame
    void Update()
    {
        if (GameManager.playerGold >= interGold)
        {
            notYetQ.SetActive(false);
        }
        else
        {
            notYetQ.SetActive(true);
        }
    }

    public void LackGoldPopUp()
    {
        Vector3 position = popUp.transform.localPosition;
        position.x = 0;
        position.y = 105;
        popUp.transform.localPosition = position;

        popText.text = "골드가 부족해!\n" + interGold + "골드는 모으고 들어가자!";
    }
}
