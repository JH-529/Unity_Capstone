using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAni : MonoBehaviour
{
    public Animator testAnim;

    // Update is called once per frame
    void Awake()
    {
    }

    public void AtkAnimation()
    {
        testAnim.SetBool("Attack", true);
    }

    public void AtkAnimationStop()
    {
        testAnim.SetBool("Attack", false);
    }

    public void BlockAnimation()
    {
        testAnim.SetBool("Block", true);
    }

    public void BlockAnimationStop()
    {
        testAnim.SetBool("Block", false);
    }

    public void DieAnimation()
    {
        testAnim.SetBool("Die", true);
    }

    public void DieAnimationStop()
    {
        testAnim.SetBool("Die", false);
    }
}
