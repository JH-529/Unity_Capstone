using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    
    public void GoBeginner()
    {
        Vector3 position = mainCamera.transform.localPosition;
        position.y = -1050;
        mainCamera.transform.localPosition = position;
        CameraScript.inMap = false;
    }

    public void GoIntermediate()
    {
        Vector3 position = mainCamera.transform.localPosition;       
        position.y = 200;
        mainCamera.transform.localPosition = position;
        CameraScript.inMap = false;
    }

    public void GoFinal()
    {
        Vector3 position = mainCamera.transform.localPosition;
        position.y = 1040;
        mainCamera.transform.localPosition = position;
        CameraScript.inMap = false;
    }

}
