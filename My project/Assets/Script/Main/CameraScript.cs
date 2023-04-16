using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Vector2 clickPoint;
    float keySpeed = 1000.0f;
    float dragSpeed = 1500.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.cameraSelect == CAMERA_TYPE.MAIN)
        {
            if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
            {
                Vector3 position = new Vector3(0, Time.deltaTime * keySpeed, 0);
                if (transform.position.y > 1050)
                {
                    position.y = -0.1f;
                }
                transform.Translate(position);
            }

            if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
            {
                Vector3 position = new Vector3(0, -Time.deltaTime * keySpeed, 0);
                if (transform.position.y < -1100)
                {
                    position.y = 0.1f;
                }
                transform.Translate(position);
            }

            /* 마우스 위치 기억 */
            if (Input.GetMouseButtonDown(0)) clickPoint = Input.mousePosition;

            if (Input.GetMouseButton(0))
            {
                /* Camera Move */
                Vector3 position = Camera.main.ScreenToViewportPoint(clickPoint - (Vector2)Input.mousePosition);
                
                Vector3 move = position * (Time.deltaTime * dragSpeed);
                move.x = 0;
                move.z = 0;

                if (transform.position.y < -1100)
                {
                    move.y = 0.1f;
                }
                if (transform.position.y > 1050)
                {
                    move.y = -0.1f;
                }

                transform.Translate(move);                
            }
        }        
    }

}
