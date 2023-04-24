using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalNotYetScript : MonoBehaviour
{
    [SerializeField] int finalLevel;
    [SerializeField] GameObject notYet;
    [SerializeField] GameObject popUp;
    [SerializeField] TextMeshProUGUI popText;    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.playerLevel >= finalLevel)
        {
            notYet.SetActive(false);
        }
    }

    public void NotYetPopUpOn()
    {
        Vector3 position = popUp.transform.localPosition;
        position.x = 0;
        position.y = 105;
        popUp.transform.localPosition = position;

        popText.text = "다음 지역으로 가려면 레벨 " + finalLevel + "은 되어야해!";
    }
}
