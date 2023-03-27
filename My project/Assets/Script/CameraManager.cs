using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera battleCamera;
    public Camera shopCamera;

    // 메인 카메라 ON
    public void OnMainCamera()
    {
        mainCamera.enabled = true;
        battleCamera.enabled = false;
        shopCamera.enabled = false;
    }

    // 전투화면 카메라 ON
    public void OnBattleCamera()
    {
        mainCamera.enabled = false;
        battleCamera.enabled = true;
        shopCamera.enabled = false;
    }

    // 상점화면 카메라 ON
    public void OnShopCamera()
    {
        mainCamera.enabled = false;
        battleCamera.enabled = false;
        shopCamera.enabled = true;
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
