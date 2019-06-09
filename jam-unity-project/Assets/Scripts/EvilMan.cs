using System.Collections;
using UnityEngine;

public class EvilMan : MonoBehaviour
{
  public Animator _animator;

  private IEnumerator _routine;

  private void Start()
  {
    _animator.SetBool("Idle", true);
  }

  public void Action()
  {
    if (_routine != null)
    {
      _animator.SetBool("Reset", true);
      StopCoroutine(_routine);
      _routine = null;
    }

    var routine = DoAction();
    StartCoroutine(routine);
    _routine = routine;
  }

  private IEnumerator DoAction()
  {
    yield return new WaitForSeconds(.1f);
    
    Debug.LogError("call action");
    
    _animator.SetBool("Reset", false);
    _animator.SetBool("Idle", false);    
    _animator.SetBool("ActionLeft", true);
    
    yield return new WaitForSeconds(1);
    
    Debug.LogError("end");
    
    _animator.SetBool("ActionLeft", false);    
    _animator.SetBool("End", true);
    
    yield return new WaitForSeconds(2f);
    
    Debug.LogError("reset");
    
    _animator.SetBool("End", false);
    _animator.SetBool("Reset", true);

    yield return new WaitForSeconds(.1f);

    _animator.SetBool("Reset", false);
    _animator.SetBool("Idle", true);

    _routine = null;
  }
}