using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMapScript : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    public void moveCamera(float value)
    {
        Vector3 position = mainCamera.transform.localPosition;
        position.y = value;
        mainCamera.transform.localPosition = position;
        CameraScript.inMap = true;
    }
}
