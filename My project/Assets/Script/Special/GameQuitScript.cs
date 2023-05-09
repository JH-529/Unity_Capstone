using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuitScript : MonoBehaviour
{
   public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
