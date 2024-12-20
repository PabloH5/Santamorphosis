using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    [SerializeField] private Animator _toolBarAnimator;


    public void SetTool(String toolName)
    {
        _toolBarAnimator.SetBool("SantaTool", false);
        _toolBarAnimator.SetBool(toolName, true);
    }
    public void SetSanta()
    {
        _toolBarAnimator.SetBool("SantaTool", true);
        _toolBarAnimator.SetBool("Tool1", false);
        _toolBarAnimator.SetBool("Tool2", false);
        _toolBarAnimator.SetBool("Tool3", false);
    }


}
