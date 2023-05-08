using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInScript : MonoBehaviour
{
    public void SetEasy()
    {
        GameManager.DifficultySetEasy();
        //Debug.Log("Set Easy");
    }

    public void SetNormal()
    {
        GameManager.DifficultySetNormal();
        //Debug.Log("Set Normal");
    }
}
