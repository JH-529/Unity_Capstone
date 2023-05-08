using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAni : MonoBehaviour
{
    public Animator anim;

    // Update is called once per frame
    void Awake()
    {
    }

    public void AtkAnimation()
    {
        anim.SetBool("Attack", true);
    }

    public void AtkAnimationStop()
    {
        anim.SetBool("Attack", false);
    }

    public void BlockAnimation()
    {
        anim.SetBool("Block", true);
    }

    public void BlockAnimationStop()
    {
        anim.SetBool("Block", false);
    }

    public void DieAnimation()
    {
        anim.SetBool("Die", true);
    }

    public void DieAnimationStop()
    {
        anim.SetBool("Die", false);
    }
}
