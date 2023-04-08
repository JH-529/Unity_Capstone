using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public Camera battleCamera;
    public Camera shopCamera;
    public Camera specialCamera;
    public Camera restCamera;
    public Camera secretRoomCamera;

    // 메인 카메라 ON
    public void OnMainCamera()
    {
        mainCamera.enabled = true;
        battleCamera.enabled = false;
        shopCamera.enabled = false;
        specialCamera.enabled = false;
        restCamera.enabled = false;
    }

    // 전투화면 카메라 ON
    public void OnBattleCamera()
    {
        mainCamera.enabled = false;
        battleCamera.enabled = true;
        shopCamera.enabled = false;
        specialCamera.enabled = false;
        restCamera.enabled = false;
    }

    // 상점화면 카메라 ON
    public void OnShopCamera()
    {
        mainCamera.enabled = false;
        battleCamera.enabled = false;
        shopCamera.enabled = true;
        specialCamera.enabled = false;
        restCamera.enabled = false;
    }

    public void OnSpecialCamera()
    {
        mainCamera.enabled = false;
        battleCamera.enabled = false;
        shopCamera.enabled = false;
        specialCamera.enabled = true;
        restCamera.enabled = false;
    }

    public void OnRestCamera()
    {
        mainCamera.enabled = false;
        battleCamera.enabled = false;
        shopCamera.enabled = false;
        specialCamera.enabled = false;
        restCamera.enabled = true;
    }

    public void OnSecretRoomCamera()
    {
        mainCamera.enabled = false;
        battleCamera.enabled = false;
        shopCamera.enabled = false;
        specialCamera.enabled = false;
        restCamera.enabled = false;
        secretRoomCamera.enabled = true;
    }
}
