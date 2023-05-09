using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SecretButtonScript : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] GameObject notYet;
    [SerializeField] GameObject popUp;
    [SerializeField] TextMeshProUGUI popText;

    // Start is called before the first frame update
    void Start()
    {
        btn.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.getKey)
        {
            btn.interactable = true;
        }
        else
        {
            btn.interactable = false;
        }

        if(GameManager.getKey)
        {
            notYet.SetActive(false);
        }
    }

    public void NotHaveKeyPopUp()
    {
        Vector3 position = popUp.transform.localPosition;
        position.x = 0;
        position.y = 105;
        popUp.transform.localPosition = position;

        popText.text = "���谡 �ʿ���..\n ���� ���ٸ� ��������..?";
    }
}
