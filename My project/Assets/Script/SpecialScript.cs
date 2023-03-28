using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialScript : MonoBehaviour
{
    public void Collect()
    {
        GameManager.playerStatus.hp += 10;
        GameManager.playerGold += 30;
        if(GameManager.playerStatus.hp > GameManager.playerStatus.maxHp)
        { GameManager.playerStatus.hp = GameManager.playerStatus.maxHp; }
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }

    public void Wrong()
    {
        GameManager.playerGold -= 20;
        if(GameManager.playerGold < 0)
        { GameManager.playerGold = 0; }
        GameManager.cameraSelect = CAMERA_TYPE.MAIN;
    }
}
