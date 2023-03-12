using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    bool isAlt;
    Vector2 clickPoint;
    float dragSpeed = 700.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 position = new Vector3(0, Time.deltaTime * dragSpeed, 0);
            transform.Translate(position);
        }

        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 position = new Vector3(0, -Time.deltaTime * dragSpeed, 0);
            transform.Translate(position);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt)) isAlt = true;
        if (Input.GetKeyUp(KeyCode.LeftAlt)) isAlt = false;

        /* 마우스 위치 기억 */
        if (Input.GetMouseButtonDown(0)) clickPoint = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            if (isAlt)
            {
                /* Camera Move */
                Vector3 position = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint);

                Vector3 move = position * (Time.deltaTime * dragSpeed);

                transform.Translate(move);
            }
        }
    }

}
