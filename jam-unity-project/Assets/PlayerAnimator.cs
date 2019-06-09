using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class PlayerAnimator : MonoBehaviour
{
  private Player Player => GetComponentInParent<Player>();
  private Animator Animator => GetComponent<Animator>();
  
  IEnumerator Start()
  {
    yield break;
//    yield break;
    while (true)
    {
      yield return null;

      if (Player.IsRun)
      {
//        if (!Animator.GetBool("Run"))
        {
          Animator.SetBool("Run", true);
          
          Debug.LogError("run true");
        }
      }
      else
      {
        Animator.SetBool("Run", false);
        //Debug.LogError("run false");
      //  Animator.SetBool("Run", false);
      }
    }
  }
}