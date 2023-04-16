using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScript : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BookTrunOver()
    {
        if(StoryScript.imageCount < 4)
        {
            StoryScript.imageCount++;
        }
        if(StoryScript.imageCount == 4)
        {
            StoryScript.imageCount = 0;            
        }
    }
}
