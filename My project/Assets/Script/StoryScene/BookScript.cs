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

    public void MovePrev()
    {       
        if (StoryScript.imageCount > 0)
        {
            StoryScript.imageCount--;

            if (StoryScript.imageCount == 0)
            {
                StoryScript.imageCount = 0;
            }
        }        
    }

    public void MoveNext()
    {           
        if (StoryScript.imageCount < StoryScript.bookPage)
        {
            StoryScript.imageCount++;
            if (StoryScript.imageCount == StoryScript.bookPage)
            {
                StoryScript.imageCount = StoryScript.bookPage - 1;
            }
        }        
    }
}
