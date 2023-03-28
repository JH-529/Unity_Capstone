using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera battleCamera;
    public Camera shopCamera;
    public Camera specialCamera;

    // ���� ī�޶� ON
    public void OnMainCamera()
    {
        mainCamera.enabled = true;
        battleCamera.enabled = false;
        shopCamera.enabled = false;
        specialCamera.enabled = false;
    }

    // ����ȭ�� ī�޶� ON
    public void OnBattleCamera()
    {
        mainCamera.enabled = false;
        battleCamera.enabled = true;
        shopCamera.enabled = false;
        specialCamera.enabled = false;
    }

    // ����ȭ�� ī�޶� ON
    public void OnShopCamera()
    {
        mainCamera.enabled = false;
        battleCamera.enabled = false;
        shopCamera.enabled = true;
        specialCamera.enabled = false;
    }

    public void OnSpecialCamera()
    {
        mainCamera.enabled = false;
        battleCamera.enabled = false;
        shopCamera.enabled = false;
        specialCamera.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
