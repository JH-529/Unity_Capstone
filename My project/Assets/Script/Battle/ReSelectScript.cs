using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSelectScript : MonoBehaviour
{
    [SerializeField] GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TranslationX(float value)
    {
        //Debug.Log("하이");
        Vector3 position = button.transform.localPosition;
        position.x = value;
        button.transform.localPosition = position;
        GameManager.turnStart = true;
    }

    public void TranslationY(float value)
    {
        //Debug.Log("하이");
        Vector3 position = button.transform.localPosition;
        position.y = value;
        button.transform.localPosition = position;
    }
}
