using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] bgms = new AudioSource[10];
    public static int playBgm = 0;

    private void Awake()
    {
        var soundManager = FindObjectsOfType<SoundManager>();
        if(soundManager.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        bgms[playBgm].Play();
    }
}
