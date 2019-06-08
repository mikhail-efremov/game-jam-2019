using System.Collections;
using UnityEngine;

public class EvilMan : MonoBehaviour
{
  public Animator _animator;

  public IEnumerator PreAction()
  {
    _animator.SetBool("Reset", false);    
    _animator.SetBool("Idle", true);

    yield return new WaitForSeconds(2);
    
    _animator.SetBool("Idle", false);    
    _animator.SetBool("ActionLeft", true);
  }

  public IEnumerator Action()
  {
    _animator.SetBool("ActionLeft", false);    
    _animator.SetBool("End", true);
    
    yield return new WaitForSeconds(2);
    
    _animator.SetBool("End", false);
    _animator.SetBool("Reset", true);
  }
}